using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DevGearbox.Utils;

public class WindowsCleaner
{
    public class CleanupItem
    {
        public string Path { get; set; } = string.Empty;
        public long Size { get; set; }
        public string Category { get; set; } = string.Empty;
        public bool CanDelete { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }

    public class CleanupResult
    {
        public List<CleanupItem> Items { get; set; } = new();
        public long TotalSize { get; set; }
        public int TotalFiles { get; set; }
        public int DeletedFiles { get; set; }
        public long DeletedSize { get; set; }
        public List<string> Errors { get; set; } = new();
    }

    public class ScanSettings
    {
        public bool IncludeWindowsTemp { get; set; } = true;
        public bool IncludePrefetch { get; set; } = true;
        public bool IncludeRecycleBin { get; set; } = false;
        public bool IncludeBrowserCaches { get; set; } = true;
    }

    /// <summary>
    /// Scans the system for temporary and unnecessary files
    /// </summary>
    public static CleanupResult ScanForCleanup(ScanSettings? settings = null)
    {
        settings ??= new ScanSettings();
        settings ??= new ScanSettings();
        var result = new CleanupResult();

        try
        {
            // Scan Windows Temp folder
            if (settings.IncludeWindowsTemp)
            {
                ScanDirectory(result, Environment.GetEnvironmentVariable("TEMP") ?? Path.GetTempPath(), "Windows Temp");

                // Scan User Temp folder (if different)
                var userTemp = Path.GetTempPath();
                if (!string.IsNullOrEmpty(userTemp))
                {
                    ScanDirectory(result, userTemp, "User Temp");
                }
            }

            // Scan Windows Prefetch (requires admin)
            if (settings.IncludePrefetch)
            {
                var prefetchPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Prefetch");
                if (Directory.Exists(prefetchPath))
                {
                    ScanDirectory(result, prefetchPath, "Prefetch", "*.pf");
                }
            }

            // Scan Recycle Bin metadata (safe to clear)
            if (settings.IncludeRecycleBin)
            {
                ScanRecycleBin(result);
            }

            // Scan browser caches
            if (settings.IncludeBrowserCaches)
            {
                ScanBrowserCaches(result);
            }

            // Calculate totals
            result.TotalFiles = result.Items.Count;
            result.TotalSize = result.Items.Sum(i => i.Size);
        }
        catch (Exception ex)
        {
            result.Errors.Add($"Scan error: {ex.Message}");
        }

        return result;
    }

    /// <summary>
    /// Deletes the scanned items
    /// </summary>
    public static CleanupResult PerformCleanup(List<CleanupItem> itemsToDelete)
    {
        var result = new CleanupResult
        {
            Items = itemsToDelete
        };

        foreach (var item in itemsToDelete)
        {
            try
            {
                if (!item.CanDelete)
                {
                    result.Errors.Add($"Skipped (locked): {item.Path}");
                    continue;
                }

                if (File.Exists(item.Path))
                {
                    File.Delete(item.Path);
                    result.DeletedFiles++;
                    result.DeletedSize += item.Size;
                }
                else if (Directory.Exists(item.Path))
                {
                    Directory.Delete(item.Path, true);
                    result.DeletedFiles++;
                    result.DeletedSize += item.Size;
                }
            }
            catch (UnauthorizedAccessException)
            {
                result.Errors.Add($"Access denied: {item.Path}");
            }
            catch (IOException ex)
            {
                result.Errors.Add($"Cannot delete (in use): {Path.GetFileName(item.Path)} - {ex.Message}");
            }
            catch (Exception ex)
            {
                result.Errors.Add($"Error deleting {item.Path}: {ex.Message}");
            }
        }

        return result;
    }

    private static void ScanDirectory(CleanupResult result, string path, string category, string searchPattern = "*.*")
    {
        if (!Directory.Exists(path))
            return;

        try
        {
            var files = Directory.GetFiles(path, searchPattern, SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {
                try
                {
                    var fileInfo = new FileInfo(file);
                    var canDelete = CanDeleteFile(file);

                    result.Items.Add(new CleanupItem
                    {
                        Path = file,
                        Size = fileInfo.Length,
                        Category = category,
                        CanDelete = canDelete,
                        ErrorMessage = canDelete ? string.Empty : "File in use or locked"
                    });
                }
                catch (Exception ex)
                {
                    result.Errors.Add($"Cannot access {file}: {ex.Message}");
                }
            }

            // Scan subdirectories (temp folders often have nested structure)
            if (category.Contains("Temp"))
            {
                var directories = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
                foreach (var dir in directories)
                {
                    try
                    {
                        var dirInfo = new DirectoryInfo(dir);
                        var size = GetDirectorySize(dir);
                        var canDelete = CanDeleteDirectory(dir);

                        result.Items.Add(new CleanupItem
                        {
                            Path = dir,
                            Size = size,
                            Category = category,
                            CanDelete = canDelete,
                            ErrorMessage = canDelete ? string.Empty : "Directory in use or locked"
                        });
                    }
                    catch (Exception ex)
                    {
                        result.Errors.Add($"Cannot access {dir}: {ex.Message}");
                    }
                }
            }
        }
        catch (UnauthorizedAccessException)
        {
            result.Errors.Add($"Access denied to {path} (may require admin privileges)");
        }
        catch (Exception ex)
        {
            result.Errors.Add($"Error scanning {path}: {ex.Message}");
        }
    }

    private static void ScanRecycleBin(CleanupResult result)
    {
        try
        {
            // Scan Recycle Bin info files (not actual deleted files)
            var drives = DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.Fixed);
            foreach (var drive in drives)
            {
                var recycleBinPath = Path.Combine(drive.Name, "$Recycle.Bin");
                if (Directory.Exists(recycleBinPath))
                {
                    ScanDirectory(result, recycleBinPath, "Recycle Bin");
                }
            }
        }
        catch (Exception ex)
        {
            result.Errors.Add($"Error scanning Recycle Bin: {ex.Message}");
        }
    }

    private static void ScanBrowserCaches(CleanupResult result)
    {
        var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        // Chrome caches (multiple cache types)
        var chromeUserDataPath = Path.Combine(localAppData, "Google", "Chrome", "User Data", "Default");
        if (Directory.Exists(chromeUserDataPath))
        {
            ScanBrowserCacheFolder(result, Path.Combine(chromeUserDataPath, "Cache"), "Chrome Cache");
            ScanBrowserCacheFolder(result, Path.Combine(chromeUserDataPath, "Code Cache"), "Chrome Code Cache");
            ScanBrowserCacheFolder(result, Path.Combine(chromeUserDataPath, "GPUCache"), "Chrome GPU Cache");
        }

        // Edge caches (multiple cache types)
        var edgeUserDataPath = Path.Combine(localAppData, "Microsoft", "Edge", "User Data", "Default");
        if (Directory.Exists(edgeUserDataPath))
        {
            ScanBrowserCacheFolder(result, Path.Combine(edgeUserDataPath, "Cache"), "Edge Cache");
            ScanBrowserCacheFolder(result, Path.Combine(edgeUserDataPath, "Code Cache"), "Edge Code Cache");
            ScanBrowserCacheFolder(result, Path.Combine(edgeUserDataPath, "GPUCache"), "Edge GPU Cache");
        }

        // Firefox cache
        var firefoxProfilesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
            "Mozilla", "Firefox", "Profiles");
        if (Directory.Exists(firefoxProfilesPath))
        {
            try
            {
                var profiles = Directory.GetDirectories(firefoxProfilesPath);
                foreach (var profile in profiles)
                {
                    ScanBrowserCacheFolder(result, Path.Combine(profile, "cache2"), "Firefox Cache");
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add($"Error scanning Firefox cache: {ex.Message}");
            }
        }
    }

    private static void ScanBrowserCacheFolder(CleanupResult result, string cachePath, string category)
    {
        if (!Directory.Exists(cachePath))
            return;

        try
        {
            // Scan all files recursively in cache folder
            var files = Directory.GetFiles(cachePath, "*.*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                try
                {
                    var fileInfo = new FileInfo(file);
                    var canDelete = CanDeleteFile(file);

                    result.Items.Add(new CleanupItem
                    {
                        Path = file,
                        Size = fileInfo.Length,
                        Category = category,
                        CanDelete = canDelete,
                        ErrorMessage = canDelete ? string.Empty : "File in use or locked"
                    });
                }
                catch (Exception ex)
                {
                    result.Errors.Add($"Cannot access {file}: {ex.Message}");
                }
            }
        }
        catch (UnauthorizedAccessException)
        {
            result.Errors.Add($"Access denied to {cachePath}");
        }
        catch (Exception ex)
        {
            result.Errors.Add($"Error scanning {cachePath}: {ex.Message}");
        }
    }

    private static bool CanDeleteFile(string filePath)
    {
        try
        {
            // Try to open the file exclusively to check if it's in use
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                return true;
            }
        }
        catch
        {
            return false;
        }
    }

    private static bool CanDeleteDirectory(string dirPath)
    {
        try
        {
            // Check if any files in the directory are in use
            var files = Directory.GetFiles(dirPath, "*", SearchOption.AllDirectories);
            return files.Length == 0 || files.All(CanDeleteFile);
        }
        catch
        {
            return false;
        }
    }

    private static long GetDirectorySize(string dirPath)
    {
        try
        {
            var dirInfo = new DirectoryInfo(dirPath);
            return dirInfo.EnumerateFiles("*", SearchOption.AllDirectories).Sum(file => file.Length);
        }
        catch
        {
            return 0;
        }
    }

    public static string FormatBytes(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        double len = bytes;
        int order = 0;
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len = len / 1024;
        }
        return $"{len:0.##} {sizes[order]}";
    }
}

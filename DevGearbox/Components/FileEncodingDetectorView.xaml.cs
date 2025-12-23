using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DevGearbox.Utils;
using Microsoft.Win32;
using Wpf.Ui.Controls;

namespace DevGearbox.Components;

public partial class FileEncodingDetectorView : UserControl
{
    private readonly SolidColorBrush _defaultBorderBrush;
    private readonly SolidColorBrush _hoverBorderBrush;
    private string _lastDetectedEncoding = "";

    public FileEncodingDetectorView()
    {
        InitializeComponent();
        _defaultBorderBrush = TryFindResource("DropBorderBrush") as SolidColorBrush
                              ?? new SolidColorBrush(Color.FromRgb(0x3E, 0x3E, 0x42));
        _hoverBorderBrush = TryFindResource("DropHoverBrush") as SolidColorBrush
                            ?? new SolidColorBrush(Color.FromRgb(0x0E, 0x63, 0x9C));
    }

    private void DropArea_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            e.Effects = DragDropEffects.Copy;
            DropBorder.BorderBrush = _hoverBorderBrush;
        }
        else
        {
            e.Effects = DragDropEffects.None;
        }

        e.Handled = true;
    }

    private void DropArea_DragOver(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            e.Effects = DragDropEffects.Copy;
            DropBorder.BorderBrush = _hoverBorderBrush;
        }
        else
        {
            e.Effects = DragDropEffects.None;
            DropBorder.BorderBrush = _defaultBorderBrush;
        }

        e.Handled = true;
    }

    private void DropArea_DragLeave(object sender, DragEventArgs e)
    {
        DropBorder.BorderBrush = _defaultBorderBrush;
        e.Handled = true;
    }

    private void DropArea_Drop(object sender, DragEventArgs e)
    {
        DropBorder.BorderBrush = _defaultBorderBrush;
        e.Handled = true;

        if (!e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            SetStatus("Please drop a valid file", InfoBarSeverity.Warning);
            return;
        }

        if (e.Data.GetData(DataFormats.FileDrop) is string[] files && files.Length > 0)
        {
            ProcessFile(files[0]);
        }
    }

    private void BrowseFile_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog
        {
            CheckFileExists = true,
            Multiselect = false,
            Title = "Select a file to analyze"
        };

        if (dialog.ShowDialog() == true)
        {
            ProcessFile(dialog.FileName);
        }
    }

    private void ProcessFile(string filePath)
    {
        try
        {
            // Update file path
            SelectedFileText.Text = filePath;
            
            // Get file info
            var fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists)
            {
                SetStatus("File not found", InfoBarSeverity.Error);
                ClearResults();
                return;
            }

            // Update file size
            FileSizeText.Text = FormatFileSize(fileInfo.Length);

            // Detect encoding with detailed info
            var detailedResult = FileEncodingDetector.DetectEncodingDetailed(filePath);
            
            if (detailedResult.IsError)
            {
                DetectedEncodingText.Text = "Error";
                BomStatusText.Text = "—";
                ConfidenceText.Text = "—";
                AnalysisText.Text = detailedResult.EncodingName;
                TechnicalDetailsText.Text = "Unable to analyze file";
                SetStatus(detailedResult.EncodingName, InfoBarSeverity.Error);
                CopyEncodingButton.IsEnabled = false;
                _lastDetectedEncoding = "";
            }
            else
            {
                // Update main encoding display
                DetectedEncodingText.Text = detailedResult.EncodingName;
                _lastDetectedEncoding = detailedResult.EncodingName;

                // Update BOM status
                BomStatusText.Text = detailedResult.HasBOM ? "✓ Yes" : "✗ No";

                // Update confidence
                ConfidenceText.Text = detailedResult.Confidence;

                // Update analysis
                AnalysisText.Text = detailedResult.Analysis;

                // Update technical details
                TechnicalDetailsText.Text = BuildTechnicalDetails(detailedResult, fileInfo);

                // Update status
                SetStatus($"Successfully detected: {detailedResult.EncodingName}", InfoBarSeverity.Success);
                
                // Enable copy button
                CopyEncodingButton.IsEnabled = true;
            }
        }
        catch (Exception ex)
        {
            SetStatus($"Error processing file: {ex.Message}", InfoBarSeverity.Error);
            ClearResults();
        }
    }

    private void CopyEncoding_Click(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(_lastDetectedEncoding))
        {
            try
            {
                Clipboard.SetText(_lastDetectedEncoding);
                SetStatus("Encoding copied to clipboard", InfoBarSeverity.Success);
            }
            catch (Exception ex)
            {
                SetStatus($"Failed to copy: {ex.Message}", InfoBarSeverity.Error);
            }
        }
    }

    private string FormatFileSize(long bytes)
    {
        string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
        int suffixIndex = 0;
        double size = bytes;

        while (size >= 1024 && suffixIndex < suffixes.Length - 1)
        {
            size /= 1024;
            suffixIndex++;
        }

        return $"{size:N2} {suffixes[suffixIndex]} ({bytes:N0} bytes)";
    }

    private string BuildTechnicalDetails(FileEncodingDetector.EncodingResult result, FileInfo fileInfo)
    {
        var details = $"File: {fileInfo.Name}\n";
        details += $"Path: {fileInfo.FullName}\n";
        details += $"Size: {fileInfo.Length:N0} bytes\n";
        details += $"Modified: {fileInfo.LastWriteTime:yyyy-MM-dd HH:mm:ss}\n";
        details += $"\nEncoding: {result.EncodingName}\n";
        details += $"BOM: {(result.HasBOM ? "Present" : "Not present")}\n";
        details += $"Confidence: {result.Confidence}\n";
        
        if (!string.IsNullOrEmpty(result.TechnicalNotes))
        {
            details += $"\nNotes:\n{result.TechnicalNotes}";
        }

        return details;
    }

    private void ClearResults()
    {
        DetectedEncodingText.Text = "—";
        FileSizeText.Text = "—";
        BomStatusText.Text = "—";
        ConfidenceText.Text = "—";
        AnalysisText.Text = "—";
        TechnicalDetailsText.Text = "No file analyzed yet";
        CopyEncodingButton.IsEnabled = false;
        _lastDetectedEncoding = "";
    }

    private void SetStatus(string message, InfoBarSeverity severity)
    {
        StatusInfoBar.Title = message;
        StatusInfoBar.Severity = severity;
    }
}

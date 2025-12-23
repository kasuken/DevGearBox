using System;
using System.IO;
using DevGearbox.Utils;
using Xunit;

namespace DevGearbox.Tests;

public class FileEncodingDetectorTests
{
    private readonly string _testFilesPath;

    public FileEncodingDetectorTests()
    {
        // Files are copied to output directory under TestFiles folder
        _testFilesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestFiles");
    }

    private string GetTestFilePath(string fileName)
    {
        var path = Path.Combine(_testFilesPath, fileName);
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Test file not found: {path}");
        }
        return path;
    }

    [Fact]
    public void Detects_Utf32_Le_Bom()
    {
        var path = GetTestFilePath("utf32le.txt");
        var result = FileEncodingDetector.DetectEncoding(path);
        Assert.Equal("UTF-32 LE with BOM", result);
    }

    [Fact]
    public void Detects_Utf32_Be_Bom()
    {
        var path = GetTestFilePath("utf32be.txt");
        var result = FileEncodingDetector.DetectEncoding(path);
        Assert.Equal("UTF-32 BE with BOM", result);
    }

    [Fact]
    public void Detects_Utf8_Bom()
    {
        var path = GetTestFilePath("utf8bom.txt");
        var result = FileEncodingDetector.DetectEncoding(path);
        Assert.Equal("UTF-8 with BOM", result);
    }

    [Fact]
    public void Detects_Utf16_Le_Bom()
    {
        var path = GetTestFilePath("utf16lebom.txt");
        var result = FileEncodingDetector.DetectEncoding(path);
        Assert.Equal("UTF-16 LE with BOM", result);
    }

    [Fact]
    public void Detects_Utf16_Be_Bom()
    {
        var path = GetTestFilePath("utf16bebom.txt");
        var result = FileEncodingDetector.DetectEncoding(path);
        Assert.Equal("UTF-16 BE with BOM", result);
    }

    [Fact]
    public void Detects_Binary_File()
    {
        var path = GetTestFilePath("binary.bin");
        var result = FileEncodingDetector.DetectEncoding(path);
        Assert.Equal("Binary file (not text)", result);
    }

    [Fact]
    public void Detects_Utf16_Le_NoBom()
    {
        var path = GetTestFilePath("utf16le_nobom.txt");
        var result = FileEncodingDetector.DetectEncoding(path);
        Assert.Equal("UTF-16 LE (no BOM)", result);
    }

    [Fact]
    public void Detects_Utf16_Be_NoBom()
    {
        var path = GetTestFilePath("utf16be_nobom.txt");
        var result = FileEncodingDetector.DetectEncoding(path);
        Assert.Equal("UTF-16 BE (no BOM)", result);
    }

    [Fact]
    public void Detects_Ascii()
    {
        var path = GetTestFilePath("ascii.txt");
        var result = FileEncodingDetector.DetectEncoding(path);
        Assert.Equal("ASCII", result);
    }

    [Fact]
    public void Detects_Utf8_NoBom()
    {
        var path = GetTestFilePath("utf8_nobom.txt");
        var result = FileEncodingDetector.DetectEncoding(path);
        Assert.Equal("UTF-8", result);
    }

    [Fact]
    public void Detects_Windows1252_Fallback()
    {
        var path = GetTestFilePath("win1252.txt");
        var result = FileEncodingDetector.DetectEncoding(path);
        Assert.Equal("Windows-1252 (fallback)", result);
    }
}

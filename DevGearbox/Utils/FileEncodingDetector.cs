using System;
using System.IO;
using System.Linq;

namespace DevGearbox.Utils;

public static class FileEncodingDetector
{
    public static string DetectEncoding(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return "Please provide a file path.";
        }

        if (!File.Exists(filePath))
        {
            return $"File not found: {filePath}";
        }

        try
        {
            var bytes = File.ReadAllBytes(filePath);
            if (bytes.Length == 0)
            {
                return "Empty file (no bytes to detect).";
            }

            return DetectEncoding(bytes);
        }
        catch (Exception ex)
        {
            return $"Unable to read file: {ex.Message}";
        }
    }

    public static string DetectEncoding(byte[] bytes)
    {
        if (bytes.Length >= 4)
        {
            if (bytes[0] == 0xFF && bytes[1] == 0xFE && bytes[2] == 0x00 && bytes[3] == 0x00)
                return "UTF-32 LE with BOM";
            if (bytes[0] == 0x00 && bytes[1] == 0x00 && bytes[2] == 0xFE && bytes[3] == 0xFF)
                return "UTF-32 BE with BOM";
        }

        if (bytes.Length >= 3 && bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF)
            return "UTF-8 with BOM";

        if (bytes.Length >= 2)
        {
            if (bytes[0] == 0xFF && bytes[1] == 0xFE)
                return "UTF-16 LE with BOM";
            if (bytes[0] == 0xFE && bytes[1] == 0xFF)
                return "UTF-16 BE with BOM";
        }

        if (IsLikelyUtf16(bytes, littleEndian: true))
            return "UTF-16 LE (no BOM)";

        if (IsLikelyUtf16(bytes, littleEndian: false))
            return "UTF-16 BE (no BOM)";

        if (IsAscii(bytes))
            return "ASCII";

        if (IsUtf8(bytes))
            return "UTF-8";

        return "Windows-1252 (fallback)";
    }

    private static bool IsAscii(byte[] bytes)
    {
        return bytes.All(b => b < 0x80);
    }

    private static bool IsUtf8(byte[] bytes)
    {
        var i = 0;
        while (i < bytes.Length)
        {
            var current = bytes[i];

            if (current <= 0x7F)
            {
                i++;
                continue;
            }

            int additionalBytes;
            if ((current & 0xE0) == 0xC0)
                additionalBytes = 1;
            else if ((current & 0xF0) == 0xE0)
                additionalBytes = 2;
            else if ((current & 0xF8) == 0xF0)
                additionalBytes = 3;
            else
                return false;

            if (i + additionalBytes >= bytes.Length)
                return false;

            for (var j = 1; j <= additionalBytes; j++)
            {
                if ((bytes[i + j] & 0xC0) != 0x80)
                    return false;
            }

            i += additionalBytes + 1;
        }

        return true;
    }

    private static bool IsLikelyUtf16(byte[] bytes, bool littleEndian)
    {
        if (bytes.Length < 2 || bytes.Length % 2 != 0)
            return false;

        var zeroOnEven = 0;
        var zeroOnOdd = 0;

        for (var i = 0; i < bytes.Length; i++)
        {
            if (i % 2 == 0 && bytes[i] == 0x00) zeroOnEven++;
            if (i % 2 == 1 && bytes[i] == 0x00) zeroOnOdd++;
        }

        var threshold = bytes.Length / 10; // allow some noise
        return littleEndian ? zeroOnOdd > threshold && zeroOnEven == 0 : zeroOnEven > threshold && zeroOnOdd == 0;
    }
}

using System;
using System.IO;
using System.Linq;

namespace DevGearbox.Utils;

public static class FileEncodingDetector
{
    private const int SampleReadLength = 8192;
    private const double Utf16DominantNullRatio = 0.30;
    private const double Utf16NoiseNullRatio = 0.10;

    public static string DetectEncoding(string filePath)
    {
        var (message, _) = DetectEncodingWithStatus(filePath);
        return message;
    }

    public static (string message, bool isError) DetectEncodingWithStatus(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return ("Please provide a file path.", true);
        }

        if (!File.Exists(filePath))
        {
            return ($"File not found: {filePath}", true);
        }

        try
        {
            var bytes = ReadSample(filePath);
            if (bytes.Length == 0)
            {
                return ("File is empty or contains no readable data.", true);
            }

            return (DetectEncoding(bytes), false);
        }
        catch (Exception ex)
        {
            return ($"Unable to read file: {ex.Message}", true);
        }
    }

    private static byte[] ReadSample(string filePath)
    {
        using var stream = File.OpenRead(filePath);
        var length = (int)Math.Min(stream.Length, SampleReadLength);
        var buffer = new byte[length];
        var read = stream.Read(buffer, 0, length);
        return buffer[..read];
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

            if (i + 1 + additionalBytes > bytes.Length)
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

    /// <summary>
    /// Heuristically detects UTF-16 without BOM by checking for a dominant presence of null bytes
    /// on either even or odd byte positions (depending on endianness) while allowing some noise
    /// on the opposite positions. Ratios are tuned to avoid false positives on UTF-8/ASCII data.
    /// </summary>
    private static bool IsLikelyUtf16(byte[] bytes, bool littleEndian)
    {
        if (bytes.Length < 2)
            return false;

        var zeroOnEven = 0;
        var zeroOnOdd = 0;

        for (var i = 0; i < bytes.Length; i++)
        {
            var isEven = (i & 1) == 0;
            if (isEven && bytes[i] == 0x00)
            {
                zeroOnEven++;
            }
            else if (!isEven && bytes[i] == 0x00)
            {
                zeroOnOdd++;
            }
        }

        var pairCount = bytes.Length / 2;
        if (pairCount == 0)
            return false;
        var evenRatio = zeroOnEven / (double)pairCount;
        var oddRatio = zeroOnOdd / (double)pairCount;

        return littleEndian
            ? oddRatio > Utf16DominantNullRatio && evenRatio < Utf16NoiseNullRatio
            : evenRatio > Utf16DominantNullRatio && oddRatio < Utf16NoiseNullRatio;
    }
}

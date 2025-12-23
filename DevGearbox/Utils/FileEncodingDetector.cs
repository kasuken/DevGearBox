using System;
using System.IO;
using System.Linq;

namespace DevGearbox.Utils;

public static class FileEncodingDetector
{
    private const int SampleReadLength = 8192;
    private const int MinBytesForUtf16Detection = 8;
    private const double Utf16DominantNullRatio = 0.35;
    private const double Utf16NoiseNullRatio = 0.05;
    private const double BinaryNullThreshold = 0.05;
    private const double BinaryControlCharThreshold = 0.10;

    public class EncodingResult
    {
        public string EncodingName { get; set; } = "";
        public bool HasBOM { get; set; }
        public string Confidence { get; set; } = "";
        public string Analysis { get; set; } = "";
        public string TechnicalNotes { get; set; } = "";
        public bool IsError { get; set; }
    }

    public static EncodingResult DetectEncodingDetailed(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return new EncodingResult
            {
                EncodingName = "Please provide a file path.",
                IsError = true
            };
        }

        if (!File.Exists(filePath))
        {
            return new EncodingResult
            {
                EncodingName = $"File not found: {filePath}",
                IsError = true
            };
        }

        try
        {
            var bytes = ReadSample(filePath);
            if (bytes.Length == 0)
            {
                return new EncodingResult
                {
                    EncodingName = "Empty file",
                    IsError = true,
                    Analysis = "File contains no readable data."
                };
            }

            return AnalyzeEncodingDetailed(bytes);
        }
        catch (Exception ex)
        {
            return new EncodingResult
            {
                EncodingName = $"Unable to read file: {ex.Message}",
                IsError = true
            };
        }
    }

    private static EncodingResult AnalyzeEncodingDetailed(byte[] bytes)
    {
        var result = new EncodingResult();

        // 1. Check for BOM signatures first (most reliable)
        if (bytes.Length >= 4)
        {
            if (bytes[0] == 0xFF && bytes[1] == 0xFE && bytes[2] == 0x00 && bytes[3] == 0x00)
            {
                result.EncodingName = "UTF-32 LE";
                result.HasBOM = true;
                result.Confidence = "Very High";
                result.Analysis = "BOM detected at file start. This is a 32-bit Unicode encoding (Little Endian).";
                result.TechnicalNotes = "BOM bytes: FF FE 00 00\nCodepage: 12000";
                return result;
            }
            if (bytes[0] == 0x00 && bytes[1] == 0x00 && bytes[2] == 0xFE && bytes[3] == 0xFF)
            {
                result.EncodingName = "UTF-32 BE";
                result.HasBOM = true;
                result.Confidence = "Very High";
                result.Analysis = "BOM detected at file start. This is a 32-bit Unicode encoding (Big Endian).";
                result.TechnicalNotes = "BOM bytes: 00 00 FE FF\nCodepage: 12001";
                return result;
            }
        }

        if (bytes.Length >= 3 && bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF)
        {
            result.EncodingName = "UTF-8";
            result.HasBOM = true;
            result.Confidence = "Very High";
            result.Analysis = "BOM detected at file start. This is the standard Unicode encoding for text files.";
            result.TechnicalNotes = "BOM bytes: EF BB BF\nCodepage: 65001\nRecommended for cross-platform text files.";
            return result;
        }

        if (bytes.Length >= 2)
        {
            if (bytes[0] == 0xFF && bytes[1] == 0xFE)
            {
                result.EncodingName = "UTF-16 LE";
                result.HasBOM = true;
                result.Confidence = "Very High";
                result.Analysis = "BOM detected at file start. This is a 16-bit Unicode encoding (Little Endian).";
                result.TechnicalNotes = "BOM bytes: FF FE\nCodepage: 1200\nCommon on Windows systems.";
                return result;
            }
            if (bytes[0] == 0xFE && bytes[1] == 0xFF)
            {
                result.EncodingName = "UTF-16 BE";
                result.HasBOM = true;
                result.Confidence = "Very High";
                result.Analysis = "BOM detected at file start. This is a 16-bit Unicode encoding (Big Endian).";
                result.TechnicalNotes = "BOM bytes: FE FF\nCodepage: 1201\nLess common than Little Endian variant.";
                return result;
            }
        }

        // 2. Check for binary file
        if (IsLikelyBinary(bytes))
        {
            result.EncodingName = "Binary";
            result.HasBOM = false;
            result.Confidence = "High";
            result.Analysis = "File contains binary data (not text). High ratio of null bytes or control characters detected.";
            result.TechnicalNotes = "This appears to be an executable, image, or other non-text file format.";
            return result;
        }

        // 3. UTF-16 without BOM detection
        if (bytes.Length >= MinBytesForUtf16Detection)
        {
            if (IsLikelyUtf16(bytes, littleEndian: true))
            {
                result.EncodingName = "UTF-16 LE";
                result.HasBOM = false;
                result.Confidence = "High";
                result.Analysis = "Heuristic analysis detected UTF-16 Little Endian pattern (no BOM). Null bytes appear predominantly on odd positions.";
                result.TechnicalNotes = "Codepage: 1200\nNote: Consider adding a BOM for better compatibility.";
                return result;
            }

            if (IsLikelyUtf16(bytes, littleEndian: false))
            {
                result.EncodingName = "UTF-16 BE";
                result.HasBOM = false;
                result.Confidence = "High";
                result.Analysis = "Heuristic analysis detected UTF-16 Big Endian pattern (no BOM). Null bytes appear predominantly on even positions.";
                result.TechnicalNotes = "Codepage: 1201\nNote: Consider adding a BOM for better compatibility.";
                return result;
            }
        }

        // 4. ASCII check
        if (IsAscii(bytes))
        {
            result.EncodingName = "ASCII";
            result.HasBOM = false;
            result.Confidence = "Very High";
            result.Analysis = "File contains only 7-bit ASCII characters (0-127). Compatible with all text encodings.";
            result.TechnicalNotes = "Codepage: 20127\nASCII is a subset of UTF-8, Windows-1252, and ISO-8859-1.";
            return result;
        }

        // 5. UTF-8 validation
        if (IsValidUtf8WithMultibyte(bytes))
        {
            result.EncodingName = "UTF-8";
            result.HasBOM = false;
            result.Confidence = "High";
            result.Analysis = "Valid UTF-8 multi-byte sequences detected. This is likely UTF-8 encoded text without BOM.";
            result.TechnicalNotes = "Codepage: 65001\nUTF-8 without BOM is common on Unix/Linux systems and modern web applications.";
            return result;
        }

        // 6. Fallback
        result.EncodingName = "Windows-1252";
        result.HasBOM = false;
        result.Confidence = "Low (Fallback)";
        result.Analysis = "Could not definitively determine encoding. File likely uses Windows-1252 or another single-byte encoding.";
        result.TechnicalNotes = "Codepage: 1252\nAlternatives: ISO-8859-1 (Latin-1), other regional codepages.\nThis is a fallback assumption based on common Windows encoding.";
        return result;
    }

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
        // 1. Check for BOM signatures first (most reliable)
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

        // 2. Check for binary file (before UTF-16 heuristics to avoid false positives)
        if (IsLikelyBinary(bytes))
            return "Binary file (not text)";

        // 3. UTF-16 without BOM detection (requires enough bytes for reliable detection)
        if (bytes.Length >= MinBytesForUtf16Detection)
        {
            if (IsLikelyUtf16(bytes, littleEndian: true))
                return "UTF-16 LE (no BOM)";

            if (IsLikelyUtf16(bytes, littleEndian: false))
                return "UTF-16 BE (no BOM)";
        }

        // 4. ASCII check (subset of UTF-8)
        if (IsAscii(bytes))
            return "ASCII";

        // 5. UTF-8 validation
        if (IsValidUtf8WithMultibyte(bytes))
            return "UTF-8";

        // 6. Fallback for extended ASCII / Windows codepage
        return "Windows-1252 (fallback)";
    }

    /// <summary>
    /// Detects likely binary files by checking for:
    /// - Scattered null bytes (not in UTF-16 pattern)
    /// - High ratio of non-printable control characters
    /// </summary>
    private static bool IsLikelyBinary(byte[] bytes)
    {
        if (bytes.Length == 0)
            return false;

        var nullCount = 0;
        var controlCharCount = 0;

        for (var i = 0; i < bytes.Length; i++)
        {
            var b = bytes[i];

            if (b == 0x00)
            {
                nullCount++;
            }
            // Control characters (0x00-0x08, 0x0E-0x1F) excluding common text chars
            // Tab (0x09), LF (0x0A), CR (0x0D) are allowed
            else if (b < 0x20 && b != 0x09 && b != 0x0A && b != 0x0D)
            {
                controlCharCount++;
            }
        }

        var nullRatio = nullCount / (double)bytes.Length;
        var controlRatio = controlCharCount / (double)bytes.Length;

        // Check for scattered nulls (binary pattern, not UTF-16)
        if (nullRatio > BinaryNullThreshold)
        {
            // Verify it's not a UTF-16 pattern by checking null distribution
            if (!HasUtf16NullPattern(bytes))
            {
                return true;
            }
        }

        // High control character ratio indicates binary
        if (controlRatio > BinaryControlCharThreshold)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Checks if null bytes follow a UTF-16 pattern (alternating positions).
    /// Returns true if nulls are predominantly on even OR odd positions.
    /// </summary>
    private static bool HasUtf16NullPattern(byte[] bytes)
    {
        if (bytes.Length < 4)
            return false;

        var zeroOnEven = 0;
        var zeroOnOdd = 0;

        for (var i = 0; i < bytes.Length; i++)
        {
            if (bytes[i] == 0x00)
            {
                if ((i & 1) == 0)
                    zeroOnEven++;
                else
                    zeroOnOdd++;
            }
        }

        var totalNulls = zeroOnEven + zeroOnOdd;
        if (totalNulls == 0)
            return false;

        // If nulls are predominantly on one side (>80%), it's likely UTF-16
        var maxRatio = Math.Max(zeroOnEven, zeroOnOdd) / (double)totalNulls;
        return maxRatio > 0.80;
    }

    private static bool IsAscii(byte[] bytes)
    {
        foreach (var b in bytes)
        {
            // Allow common control chars: Tab, LF, CR
            if (b >= 0x80 || (b < 0x20 && b != 0x09 && b != 0x0A && b != 0x0D))
            {
                // Exception: allow null only if very rare (could be padding)
                if (b != 0x00)
                    return false;
            }
        }
        return bytes.All(b => b < 0x80);
    }

    /// <summary>
    /// Validates UTF-8 encoding AND requires at least one multi-byte sequence.
    /// This distinguishes UTF-8 from pure ASCII.
    /// </summary>
    private static bool IsValidUtf8WithMultibyte(byte[] bytes)
    {
        var i = 0;
        var hasMultibyte = false;

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
            {
                additionalBytes = 1;
                // Reject overlong encoding for 2-byte sequences
                if ((current & 0x1E) == 0)
                    return false;
            }
            else if ((current & 0xF0) == 0xE0)
            {
                additionalBytes = 2;
            }
            else if ((current & 0xF8) == 0xF0)
            {
                additionalBytes = 3;
                // Reject codepoints > U+10FFFF
                if (current > 0xF4)
                    return false;
            }
            else
            {
                return false;
            }

            if (i + additionalBytes >= bytes.Length)
                return false;

            for (var j = 1; j <= additionalBytes; j++)
            {
                if ((bytes[i + j] & 0xC0) != 0x80)
                    return false;
            }

            hasMultibyte = true;
            i += additionalBytes + 1;
        }

        return hasMultibyte;
    }

    /// <summary>
    /// Heuristically detects UTF-16 without BOM by checking for a dominant presence of null bytes
    /// on either even or odd byte positions (depending on endianness) while requiring very low
    /// noise on the opposite positions. Also validates that non-null bytes are printable.
    /// </summary>
    private static bool IsLikelyUtf16(byte[] bytes, bool littleEndian)
    {
        if (bytes.Length < MinBytesForUtf16Detection)
            return false;

        var zeroOnEven = 0;
        var zeroOnOdd = 0;
        var suspiciousControlChars = 0;

        for (var i = 0; i < bytes.Length; i++)
        {
            var b = bytes[i];
            var isEven = (i & 1) == 0;

            if (b == 0x00)
            {
                if (isEven)
                    zeroOnEven++;
                else
                    zeroOnOdd++;
            }
            else
            {
                // In valid UTF-16 text, the low byte should mostly be printable ASCII
                // Check the byte that should contain the character value
                var isLowByte = littleEndian ? isEven : !isEven;
                if (isLowByte && b < 0x20 && b != 0x09 && b != 0x0A && b != 0x0D)
                {
                    suspiciousControlChars++;
                }
            }
        }

        var pairCount = bytes.Length / 2;
        if (pairCount == 0)
            return false;

        // Too many control characters suggests binary, not text
        if (suspiciousControlChars > pairCount * 0.05)
            return false;

        var evenRatio = zeroOnEven / (double)pairCount;
        var oddRatio = zeroOnOdd / (double)pairCount;

        // For UTF-16 LE: nulls should be in odd positions (high byte of ASCII chars)
        // For UTF-16 BE: nulls should be in even positions (high byte of ASCII chars)
        return littleEndian
            ? oddRatio > Utf16DominantNullRatio && evenRatio < Utf16NoiseNullRatio
            : evenRatio > Utf16DominantNullRatio && oddRatio < Utf16NoiseNullRatio;
    }
}

using System;
using System.Security.Cryptography;
using System.Text;
namespace DevGearbox.Utils;
public static class HashGenerator
{
    public static string GenerateMD5(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;
        try
        {
            using var md5 = MD5.Create();
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
    public static string GenerateSHA1(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;
        try
        {
            using var sha1 = SHA1.Create();
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = sha1.ComputeHash(inputBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
    public static string GenerateSHA256(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;
        try
        {
            using var sha256 = SHA256.Create();
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = sha256.ComputeHash(inputBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
    public static string GenerateSHA512(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;
        try
        {
            using var sha512 = SHA512.Create();
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = sha512.ComputeHash(inputBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
}

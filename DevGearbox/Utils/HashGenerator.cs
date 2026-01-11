using System;
using System.Security.Cryptography;
using System.Text;
namespace DevGearbox.Utils;
public static class HashGenerator
{
    public static string GenerateMD5(string input) => GenerateHash(input, MD5.Create);

    public static string GenerateSHA1(string input) => GenerateHash(input, SHA1.Create);

    public static string GenerateSHA256(string input) => GenerateHash(input, SHA256.Create);

    public static string GenerateSHA512(string input) => GenerateHash(input, SHA512.Create);

    private static string GenerateHash(string input, Func<HashAlgorithm> algorithmFactory)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;
        try
        {
            using var algorithm = algorithmFactory();
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
}


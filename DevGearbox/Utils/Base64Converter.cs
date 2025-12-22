using System;
using System.Text;
namespace DevGearbox.Utils;
public static class Base64Converter
{
    public static string Encode(string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;
        var bytes = Encoding.UTF8.GetBytes(text);
        return Convert.ToBase64String(bytes);
    }
    public static string Decode(string base64)
    {
        if (string.IsNullOrEmpty(base64))
            return string.Empty;
        try
        {
            var bytes = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(bytes);
        }
        catch (FormatException)
        {
            return "Invalid Base64 string";
        }
    }
}

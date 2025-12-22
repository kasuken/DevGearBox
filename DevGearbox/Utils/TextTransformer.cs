using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace DevGearbox.Utils;
public static class TextTransformer
{
    public static string ToUpperCase(string text) => text?.ToUpper() ?? string.Empty;
    public static string ToLowerCase(string text) => text?.ToLower() ?? string.Empty;
    public static string ToPascalCase(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;
        var words = Regex.Split(text, @"[\s_-]+");
        var sb = new StringBuilder();
        foreach (var word in words.Where(w => !string.IsNullOrEmpty(w)))
        {
            sb.Append(char.ToUpper(word[0]));
            if (word.Length > 1)
                sb.Append(word.Substring(1).ToLower());
        }
        return sb.ToString();
    }
    public static string ToCamelCase(string text)
    {
        var pascalCase = ToPascalCase(text);
        if (string.IsNullOrEmpty(pascalCase))
            return string.Empty;
        return char.ToLower(pascalCase[0]) + pascalCase.Substring(1);
    }
    public static string ToSnakeCase(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;
        var result = Regex.Replace(text, @"([A-Z])", "_$1").Trim('_');
        result = Regex.Replace(result, @"[\s-]+", "_");
        return result.ToLower();
    }
    public static string ToKebabCase(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;
        var result = Regex.Replace(text, @"([A-Z])", "-$1").Trim('-');
        result = Regex.Replace(result, @"[\s_]+", "-");
        return result.ToLower();
    }
    public static string Reverse(string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;
        var charArray = text.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
    public static string RemoveWhitespace(string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;
        return Regex.Replace(text, @"\s+", "");
    }
    public static string UrlEncode(string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;
        return Uri.EscapeDataString(text);
    }
    public static string UrlDecode(string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;
        return Uri.UnescapeDataString(text);
    }
}

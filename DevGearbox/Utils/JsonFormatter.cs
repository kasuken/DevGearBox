using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
namespace DevGearbox.Utils;
public static class JsonFormatter
{
    public static string Format(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return string.Empty;
        try
        {
            var parsedJson = JToken.Parse(json);
            return parsedJson.ToString(Formatting.Indented);
        }
        catch (JsonReaderException ex)
        {
            return $"Invalid JSON: {ex.Message}";
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
    public static string Minify(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return string.Empty;
        try
        {
            var parsedJson = JToken.Parse(json);
            return parsedJson.ToString(Formatting.None);
        }
        catch (JsonReaderException ex)
        {
            return $"Invalid JSON: {ex.Message}";
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
}

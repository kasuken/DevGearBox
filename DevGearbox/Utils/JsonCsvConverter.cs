using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DevGearbox.Utils;

public static class JsonCsvConverter
{
    public static string JsonToCsv(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
            throw new ArgumentException("JSON input is empty");

        try
        {
            var token = JToken.Parse(json);
            
            if (token is JArray array)
            {
                return ConvertArrayToCsv(array);
            }
            else if (token is JObject obj)
            {
                // Single object - convert to array with one item
                var singleItemArray = new JArray { obj };
                return ConvertArrayToCsv(singleItemArray);
            }
            else
            {
                throw new ArgumentException("JSON must be an object or array");
            }
        }
        catch (JsonReaderException ex)
        {
            throw new ArgumentException($"Invalid JSON: {ex.Message}");
        }
    }

    private static string ConvertArrayToCsv(JArray array)
    {
        if (array.Count == 0)
            return string.Empty;

        var csv = new StringBuilder();
        var headers = new HashSet<string>();

        // Collect all unique property names (headers)
        foreach (var item in array)
        {
            if (item is JObject obj)
            {
                foreach (var prop in obj.Properties())
                {
                    headers.Add(prop.Name);
                }
            }
        }

        var headerList = headers.ToList();
        headerList.Sort();

        // Write header row
        csv.AppendLine(string.Join(",", headerList.Select(EscapeCsvValue)));

        // Write data rows
        foreach (var item in array)
        {
            if (item is JObject obj)
            {
                var values = headerList.Select(header =>
                {
                    var value = obj[header];
                    return value != null ? EscapeCsvValue(value.ToString()) : "";
                });
                csv.AppendLine(string.Join(",", values));
            }
        }

        return csv.ToString();
    }

    public static string CsvToJson(string csv, bool prettyPrint = true)
    {
        if (string.IsNullOrWhiteSpace(csv))
            throw new ArgumentException("CSV input is empty");

        var lines = csv.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        
        if (lines.Length < 2)
            throw new ArgumentException("CSV must have at least a header row and one data row");

        var headers = ParseCsvLine(lines[0]);
        var jsonArray = new JArray();

        for (int i = 1; i < lines.Length; i++)
        {
            var values = ParseCsvLine(lines[i]);
            
            if (values.Length != headers.Length)
                continue; // Skip malformed rows

            var jsonObject = new JObject();
            for (int j = 0; j < headers.Length; j++)
            {
                var value = values[j];
                
                // Try to parse as number
                if (double.TryParse(value, out double number))
                {
                    jsonObject[headers[j]] = number;
                }
                // Try to parse as boolean
                else if (bool.TryParse(value, out bool boolean))
                {
                    jsonObject[headers[j]] = boolean;
                }
                // Keep as string
                else
                {
                    jsonObject[headers[j]] = value;
                }
            }
            jsonArray.Add(jsonObject);
        }

        return jsonArray.ToString(prettyPrint ? Formatting.Indented : Formatting.None);
    }

    private static string[] ParseCsvLine(string line)
    {
        var result = new List<string>();
        var current = new StringBuilder();
        bool inQuotes = false;

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (c == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    // Escaped quote
                    current.Append('"');
                    i++; // Skip next quote
                }
                else
                {
                    // Toggle quotes
                    inQuotes = !inQuotes;
                }
            }
            else if (c == ',' && !inQuotes)
            {
                // End of field
                result.Add(current.ToString());
                current.Clear();
            }
            else
            {
                current.Append(c);
            }
        }

        // Add last field
        result.Add(current.ToString());

        return result.ToArray();
    }

    private static string EscapeCsvValue(string value)
    {
        if (string.IsNullOrEmpty(value))
            return "";

        // If value contains comma, quote, or newline, wrap in quotes
        if (value.Contains(",") || value.Contains("\"") || value.Contains("\n") || value.Contains("\r"))
        {
            // Escape quotes by doubling them
            value = value.Replace("\"", "\"\"");
            return $"\"{value}\"";
        }

        return value;
    }
}


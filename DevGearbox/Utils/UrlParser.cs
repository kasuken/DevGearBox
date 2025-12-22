using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevGearbox.Utils;

public class UrlParseResult
{
    public string Scheme { get; set; } = "";
    public string Host { get; set; } = "";
    public string Port { get; set; } = "";
    public string Path { get; set; } = "";
    public string Query { get; set; } = "";
    public string Fragment { get; set; } = "";
    public Dictionary<string, string> Parameters { get; set; } = new();
    public string Error { get; set; } = "";
}

public static class UrlParser
{
    public static UrlParseResult Parse(string url)
    {
        var result = new UrlParseResult();

        if (string.IsNullOrWhiteSpace(url))
        {
            result.Error = "URL is empty";
            return result;
        }

        try
        {
            // Try to parse as URI
            Uri uri;
            
            // If URL doesn't have a scheme, add http://
            if (!url.Contains("://"))
            {
                url = "http://" + url;
            }

            if (!Uri.TryCreate(url, UriKind.Absolute, out uri))
            {
                result.Error = "Invalid URL format";
                return result;
            }

            // Extract components
            result.Scheme = uri.Scheme;
            result.Host = uri.Host;
            result.Port = uri.Port.ToString();
            result.Path = uri.AbsolutePath;
            result.Query = uri.Query.TrimStart('?');
            result.Fragment = uri.Fragment.TrimStart('#');

            // Parse query parameters
            if (!string.IsNullOrEmpty(uri.Query))
            {
                var queryString = uri.Query.TrimStart('?');
                var parameters = ParseQueryString(queryString);
                result.Parameters = parameters;
            }

            return result;
        }
        catch (Exception ex)
        {
            result.Error = $"Error parsing URL: {ex.Message}";
            return result;
        }
    }

    private static Dictionary<string, string> ParseQueryString(string query)
    {
        var parameters = new Dictionary<string, string>();

        if (string.IsNullOrWhiteSpace(query))
            return parameters;

        var pairs = query.Split('&');
        
        foreach (var pair in pairs)
        {
            if (string.IsNullOrWhiteSpace(pair))
                continue;

            var keyValue = pair.Split('=');
            
            if (keyValue.Length >= 1)
            {
                var key = Uri.UnescapeDataString(keyValue[0]);
                var value = keyValue.Length >= 2 
                    ? Uri.UnescapeDataString(keyValue[1]) 
                    : "";
                
                // Handle duplicate keys by appending
                if (parameters.ContainsKey(key))
                {
                    parameters[key] += ", " + value;
                }
                else
                {
                    parameters[key] = value;
                }
            }
        }

        return parameters;
    }
}


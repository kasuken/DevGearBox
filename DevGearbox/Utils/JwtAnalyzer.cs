using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DevGearbox.Utils;

public class JwtDebugResult
{
    public string Header { get; set; } = "";
    public string Payload { get; set; } = "";
    public string Signature { get; set; } = "";
    public string Error { get; set; } = "";
}

public static class JwtAnalyzer
{
    public static string Analyze(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return string.Empty;

        try
        {
            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token))
            {
                return "Invalid JWT token format";
            }

            var jwtToken = handler.ReadJwtToken(token);
            var sb = new StringBuilder();

            sb.AppendLine("=== JWT Header ===");
            sb.AppendLine(JsonConvert.SerializeObject(jwtToken.Header, Formatting.Indented));
            sb.AppendLine();

            sb.AppendLine("=== JWT Payload ===");
            sb.AppendLine(jwtToken.Payload.SerializeToJson());
            sb.AppendLine();

            sb.AppendLine("=== Token Information ===");
            sb.AppendLine($"Issuer: {jwtToken.Issuer}");
            sb.AppendLine($"Audience: {string.Join(", ", jwtToken.Audiences)}");

            if (jwtToken.ValidFrom != DateTime.MinValue)
                sb.AppendLine($"Valid From: {jwtToken.ValidFrom.ToLocalTime()}");

            if (jwtToken.ValidTo != DateTime.MinValue)
            {
                sb.AppendLine($"Valid To: {jwtToken.ValidTo.ToLocalTime()}");
                sb.AppendLine($"Expired: {jwtToken.ValidTo < DateTime.UtcNow}");
            }

            return sb.ToString();
        }
        catch (Exception ex)
        {
            return $"Error analyzing JWT: {ex.Message}";
        }
    }

    public static JwtDebugResult DebugJwt(string token)
    {
        var result = new JwtDebugResult();

        if (string.IsNullOrWhiteSpace(token))
        {
            result.Error = "Token is empty";
            return result;
        }

        try
        {
            // Split token into parts
            var parts = token.Trim().Split('.');
            
            if (parts.Length != 3)
            {
                result.Error = $"Invalid JWT format. Expected 3 parts separated by dots, found {parts.Length} parts.";
                return result;
            }

            // Decode Header
            try
            {
                var headerJson = Base64UrlDecode(parts[0]);
                var headerObj = JObject.Parse(headerJson);
                result.Header = headerObj.ToString(Formatting.Indented);
            }
            catch (Exception ex)
            {
                result.Header = $"Error decoding header: {ex.Message}\n\nRaw: {parts[0]}";
            }

            // Decode Payload
            try
            {
                var payloadJson = Base64UrlDecode(parts[1]);
                var payloadObj = JObject.Parse(payloadJson);
                result.Payload = payloadObj.ToString(Formatting.Indented);
            }
            catch (Exception ex)
            {
                result.Payload = $"Error decoding payload: {ex.Message}\n\nRaw: {parts[1]}";
            }

            // Signature (base64url encoded, not decoded)
            result.Signature = parts[2];

            return result;
        }
        catch (Exception ex)
        {
            result.Error = $"Error processing JWT: {ex.Message}";
            return result;
        }
    }

    private static string Base64UrlDecode(string base64Url)
    {
        // Convert Base64Url to Base64
        var base64 = base64Url.Replace('-', '+').Replace('_', '/');
        
        // Add padding if needed
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }

        // Decode Base64 to bytes
        var bytes = Convert.FromBase64String(base64);
        
        // Convert bytes to string
        return Encoding.UTF8.GetString(bytes);
    }
}


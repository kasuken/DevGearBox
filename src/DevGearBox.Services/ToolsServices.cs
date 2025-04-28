using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DevGearBox.Services
{
    public class ToolsServices
    {

        public string FormatJson(string json)
        {
            try
            {
                var parsedJson = JToken.Parse(json);
                return parsedJson.ToString(Newtonsoft.Json.Formatting.Indented);
            }
            catch (JsonReaderException)
            {
                return "Invalid JSON format.";
            }
        }

        public bool ValidateJson(string json)
        {
            try
            {
                JToken.Parse(json);
                return true;  // JSON is valid
            }
            catch (JsonReaderException)
            {
                return false;  // JSON is invalid
            }
        }

        public string ConvertJsonToYaml(string json)
        {
            try
            {
                var parsedJson = JToken.Parse(json);
                var yaml = new YamlDotNet.Serialization.Serializer().Serialize(parsedJson);
                return yaml;
            }
            catch (JsonReaderException)
            {
                return "Invalid JSON format.";
            }
        }

        public string EncodeBase64(string input)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(input);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public string DecodeBase64(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public string UrlEncode(string input)
        {
            return System.Net.WebUtility.UrlEncode(input);
        }

        public string UrlDecode(string input)
        {
            return System.Net.WebUtility.UrlDecode(input);
        }
    }
}

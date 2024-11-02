using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DevGearBox.Services
{
    public class ToolsServices
    {

        public string FormaJson(string json)
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
    }
}

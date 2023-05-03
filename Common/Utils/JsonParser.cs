using Newtonsoft.Json;

namespace Common.Utils
{
    public class JsonParser<T>
    {
        public static T Parse(string text) => JsonConvert.DeserializeObject<T>(text);
    }
}

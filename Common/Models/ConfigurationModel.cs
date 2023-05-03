using Newtonsoft.Json;

namespace Common.Models
{
    public class ConfigurationModel
    {
        public ConfigurationModel(string connectionString)
        {
            ConnectionString = connectionString;
        }

        [JsonProperty("connectionString")]
        public string ConnectionString { get; private set; }
    }
}

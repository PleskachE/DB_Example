using Newtonsoft.Json;

namespace MongoDb.Example.Common.Models
{
    public class MongoDbConfigurationModel
    {
        public MongoDbConfigurationModel(string server, string dataBase)
        {
            Server = server;
            DataBase = dataBase;
        }

        [JsonProperty("server")]
        public string Server { get; private set; }

        [JsonProperty("dataBase")]
        public string DataBase { get; private set; }
    }
}

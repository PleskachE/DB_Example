using Common.Constants;
using Common.Utils;
using MongoDb.Example.Common.Models;

namespace Common.Configurations
{
    public class MongoDbConfiguration
    {
        private static MongoDbConfigurationModel _configModel =>
            JsonParser<MongoDbConfigurationModel>.Parse(FileReader.ReadAllText(PathToFiles.MongoDbConfigurationFile));

        public static string MongoDbServer => _configModel.Server;

        public static string MongoDbName => _configModel.DataBase;
    }
}

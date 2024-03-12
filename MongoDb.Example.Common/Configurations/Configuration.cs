using MongoDb.Example.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDb.Example.Common.Configurations
{
    public class Configuration
    {
        private static MongoDbConfigurationModel _configModel =>
            JsonParser<MongoDbConfigurationModel>.Parse(FileReader.ReadAllText(PathToFiles.ConfigurationFile));

        public static string ConnectionString => !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("connectionString"))
                                                 ? Environment.GetEnvironmentVariable("connectionString")
                                                 : _configModel.ConnectionString;
    }
}

using Common.Constants;
using Common.Models;
using Common.Utils;
using System;

namespace Common.Configurations
{
    public class Configuration
    {
        private static ConfigurationModel _configModel =>
            JsonParser<ConfigurationModel>.Parse(FileReader.ReadAllText(PathToFiles.ConfigurationFile));

        public static string ConnectionString => 
            !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("connectionString"))
            ? Environment.GetEnvironmentVariable("connectionString")
            : _configModel.ConnectionString;
    }
}

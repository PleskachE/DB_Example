using Common.Configurations;
using MongoDB.Driver;

namespace MongoDB.Example.Utils
{
    public class MongoDbConnector
    {
        private static MongoDbConnector _instance;
        public IMongoDatabase? Database;

        private MongoDbConnector() 
        {
            var client = new MongoClient(MongoDbConfiguration.MongoDbServer);
            Database = client.GetDatabase(MongoDbConfiguration.MongoDbName);
        }

        public static MongoDbConnector Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new MongoDbConnector();
                }
                return _instance; 
            }
        }
    }
}

using Common.Configurations;
using MySql.Data.MySqlClient;

namespace DapperExample.Utils
{
    public class MyDbConnector
    {
        private static MySqlConnection _connection;

        private MyDbConnector() { }

        public static MySqlConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new MySqlConnection(Configuration.ConnectionString);
            }
            return _connection;
        }
    }
}

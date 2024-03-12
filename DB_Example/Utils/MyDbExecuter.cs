using Common.Configurations;
using DB_Example.Constants;
using MySql.Data.MySqlClient;
using System.Text;

namespace DB_Example.Utils
{
    class MyDbExecuter
    {
        private MyDbExecuter() { }

        private static MyDbExecuter _instance;
        private static MySqlConnection _connection;

        public static MyDbExecuter GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MyDbExecuter();
                _connection = new MySqlConnection(Configuration.ConnectionString);
            }
            return _instance;
        }
        public void ExecuteCommandInput(string sqlQuery)
        {
            _connection.Open();
           new MySqlCommand(sqlQuery, _connection).ExecuteReader();      
            _connection.Close();
        }

        public string ExecuteCommandRead(string sqlQuery)
        {
            _connection.Open();
            MySqlCommand command = new MySqlCommand(sqlQuery, _connection);
            MySqlDataReader reader = command.ExecuteReader();
            var sb = new StringBuilder();
            while (reader.Read())
            {
                for(var i = 0; i < reader.FieldCount; i++)
                {
                    sb.Append(reader[i] + TextConstants.ColumnSeparator);
                }
                sb.Append(TextConstants.RowSeparator);
            }
            reader.Close();
            _connection.Close();
            return sb.ToString();
        }
    }
}

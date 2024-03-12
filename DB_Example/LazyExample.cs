using MySql.Data.MySqlClient;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace DB_Example
{
    public class LazyExample
    {
        [Test]
        public void LazyTest()
        {
            LazyAuthor author = null;

            using (MySqlConnection connection = new MySqlConnection("Port=3306;Server=127.0.0.1;Database=union_reporting;user=root"))
            {
                string query = "SELECT * FROM author WHERE id = '30'";
                MySqlCommand command = new MySqlCommand(query, connection);

                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    author = new LazyAuthor()
                    {
                        id = Convert.ToInt32(reader["id"]),
                        name = reader["name"].ToString(),
                        login = reader["login"].ToString(),
                        email = reader["email"].ToString()
                    };
                }
                reader.Close();
            }

            var tests = author.Tests.Value;
        }
    }

    class LazyTest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public string MethodName { get; set; }
        public int ProjectId { get; set; }
        public int SessionId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Env { get; set; }
        public string Browser { get; set; }
        public int AuthorId { get; set; }
    }

    class LazyAuthor
    {
        public LazyAuthor() { Tests = new Lazy<List<LazyTest>>(LoadTests); }

        public LazyAuthor(string name, string login, string email)
        {
            this.name = name;
            this.login = login;
            this.email = email;
            Tests = new Lazy<List<LazyTest>>(LoadTests);
        }

        public LazyAuthor(int id, string name, string login, string email)
        {
            this.id = id;
            this.name = name;
            this.login = login;
            this.email = email;
            Tests = new Lazy<List<LazyTest>>(LoadTests);
        }

        public int id { get; set; }
        public string name { get; set; }
        public string login { get; set; }
        public string email { get; set; }

        public Lazy<List<LazyTest>> Tests { get; set; }

        private List<LazyTest> LoadTests()
        {
            List<LazyTest> items = new List<LazyTest>();

            using (MySqlConnection connection = new MySqlConnection("Port=3306;Server=127.0.0.1;Database=union_reporting;user=root"))
            {
                string query = "SELECT * FROM test WHERE author_id = @author_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@author_id", id);

                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    LazyTest item = new LazyTest()
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Name = reader["name"].ToString(),
                        StatusId = int.Parse(reader["status_id"].ToString()),
                        MethodName = reader["method_name"].ToString(),
                        ProjectId = int.Parse(reader["project_id"].ToString()),
                        SessionId = int.Parse(reader["session_id"].ToString()),
                        StartTime = DateTime.Parse(reader["start_time"].ToString()),
                        EndTime = DateTime.Parse(reader["end_time"].ToString()),
                        Env = reader["env"].ToString(),
                        Browser = reader["browser"].ToString(),
                        AuthorId = int.Parse(reader["author_id"].ToString())
                    };
                    items.Add(item);
                }
                reader.Close();
            }
            return items;
        }
    }
}

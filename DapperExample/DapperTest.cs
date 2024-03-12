using Dapper;
using DapperExample.Models.DbModel;
using DapperExample.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DapperExample
{
    public class DapperTests
    {
        [Test]
        public void DapperTest()
        {
            var sql = "SELECT * FROM test JOIN author ON test.author_id = author.id WHERE author.email = '{0}' ORDER BY test.name, author.email;";
            var tests = ComplexQueryExample(string.Format(sql, "Old_a1qa_test3@email.com"));

            tests.ToList().Add(new Test() { });
            var a = new Author();
            tests.ToList().ForEach(x => x.Author = a);

            TransactionExample();
        }

        private IEnumerable<Test> ComplexQueryExample(string sql) =>
            MyDbConnector.GetConnection().Query<Test, Author, Test>(sql, (test, author) =>
            {
                test.Author = author;
                return test;
            });

        private void TransactionExample()
        {
            using (var connection = MyDbConnector.GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        connection.Execute("NSERT INTO author (name, login, email) VALUES ('@Name', '@Login', '@Email');",
                            new { Name = "QA", Login = "a1qa_test", Email = "a1qa_test@email.com" },
                            transaction);
                        connection.Execute("UPDATE author SET name = '@Name' WHERE login = '@Login';",
                            new { Name = "AQA", Login = "a1qa_test@email.com" },
                            transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
    }
}
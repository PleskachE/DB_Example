using Dapper;
using DapperExample.Models.DbModel;
using DapperExample.Utils;
using NUnit.Framework;
using System;

namespace DapperExample
{
    public class DapperTests
    {
        [Test]
        public void DapperTest()
        {
            TransactionExample();
        }

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
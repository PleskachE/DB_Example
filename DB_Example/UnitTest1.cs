using DB_Example.Constants;
using DB_Example.Models.DbModel;
using DB_Example.SqlQueries;
using DB_Example.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DB_Example
{
    public class Tests
    {
        [Test]
        public void TestOfDbConnector()
        {
            var text = MyDbExecuter.GetInstance().ExecuteCommandRead(SqlQuery.SelectAllAuthors);
            var authors = new List<Author>();
            foreach(var item in text.Split(TextConstants.RowSeparator))
            {
                if(!string.IsNullOrEmpty(item))
                {
                    var args = item.Split(TextConstants.ColumnSeparator);
                    authors.Add(new Author(id: int.Parse(args[0]), name: args[1], login: args[2], email: args[3]));
                }
            }

            var author = authors.FirstOrDefault(item => item.name == "engineer3");

            string textOfTest = MyDbExecuter.GetInstance().ExecuteCommandRead(SqlQuery.SelectTestsByAuthorId(author.id));

            var tests = new List<Test>();
            foreach (var item in textOfTest.Split(TextConstants.RowSeparator))
            {
                if (!string.IsNullOrEmpty(item))
                {
                    var args = item.Split(TextConstants.ColumnSeparator);
                    tests.Add(new Test() 
                    {
                        Id = int.Parse(args[0]),
                        Name = args[1],
                        StatusId = int.Parse(args[2]),
                        MethodName = args[3],
                        ProjectId = int.Parse(args[4]),
                        SessionId = int.Parse(args[5]),
                        StartTime = DateTime.Parse(args[6]),
                        EndTime = DateTime.Parse(args[7]),
                        Env = args[8],
                        Browser = args[9],
                        AuthorId = int.Parse(args[10])
                    });
                }
            }
        }
    }
}
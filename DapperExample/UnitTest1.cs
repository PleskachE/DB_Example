using Dapper;
using DapperExample.Models.DbModel;
using DapperExample.SqlQueries;
using DapperExample.Utils;
using NUnit.Framework;
using System.Linq;

namespace DapperExample
{
    public class Tests
    {

        [Test]
        public void Test1()
        {
            var authors = MyDbConnector.GetConnection().Query<Author>(SqlQuery.SelectAllAuthors);
            var author = authors.AsList().FirstOrDefault(item => item.name == "engineer3");
            var tests = MyDbConnector.GetConnection().Query<Test>(SqlQuery.SelectTestsByAuthorId(author.id));
        }
    }
}
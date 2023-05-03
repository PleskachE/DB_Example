using DapperExample.Enums;
using DapperExample.Models.DbModel;
using DapperExample.Utils;
using NUnit.Framework;
using TechTalk.SpecFlow;
using Dapper;
using DapperExample.SqlQueries;
using System.Linq;

namespace DapperExample.Steps
{
    [Binding]
    public class DbSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public DbSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [When(@"Create and add to database new author")]
        public void CreateNewAuthor(Author author)
        {
            MyDbConnector.GetConnection().Execute(SqlQuery.InsertNewAuthor(author.name, author.login, author.email));
            _scenarioContext.Add(ScenarioContextStorage.CreatedAuthor.ToString(), author);
        }

        [Then(@"Created author '(is|is not)' exist in database")]
        public void CheckAuthorExistDataBase(bool isExist)
        {
            var createdAuthor = (Author)_scenarioContext[ScenarioContextStorage.CreatedAuthor.ToString()];
            Assert.AreEqual(isExist, MyDbConnector.GetConnection().Query<Author>(SqlQuery.SelectAuthorByEmail(createdAuthor.email)).ToList().Count != 0,
                $"Author <{createdAuthor.name}> is{(isExist ? " not" : string.Empty)} exist in database!");
            if (isExist)
            {
                _scenarioContext[ScenarioContextStorage.CreatedAuthor.ToString()] = (Author)MyDbConnector.GetConnection().Query<Author>(SqlQuery.SelectAuthorByEmail(createdAuthor.email)).AsList().First();
            }
        }

        [When(@"Update property '(.*)' of created author in database as a '(.*)'")]
        public void UpdateAuthorFieldInDatabase(string propertyName, string text)
        {
            var createdAuthor = (Author)_scenarioContext[ScenarioContextStorage.CreatedAuthor.ToString()];
            MyDbConnector.GetConnection().Execute(SqlQuery.UpdateAuthorById(propertyName, text, createdAuthor.id));
            createdAuthor = (Author)MyDbConnector.GetConnection().Query<Author>(SqlQuery.SelectAuthorById(createdAuthor.id)).AsList().First();
            _scenarioContext[ScenarioContextStorage.CreatedAuthor.ToString()] = createdAuthor;
        }

        [Then(@"Property '(.*)' of created author in database '(is|is not)' equal to '(.*)'")]
        public void CheckValueOfPropertyCreatedAuthor(string propertyName, bool isEqual, string value)
        {
            var property = typeof(Author).GetProperty(propertyName);
            var createdAuthor = (Author)_scenarioContext[ScenarioContextStorage.CreatedAuthor.ToString()];
            var author = (Author)MyDbConnector.GetConnection().Query<Author>(SqlQuery.SelectAuthorById(createdAuthor.id)).AsList().First();
            var valueOfProperty = property.GetValue(author);
            Assert.AreEqual(isEqual, valueOfProperty.Equals(value),
               $"Property <{propertyName}> of created author in database <{createdAuthor.name}> is{(isEqual ? " not" : string.Empty)} equal to {value}!");
        }

        [When(@"Delete created author from database")]
        public void DeleteCreatedAuthor()
        {
            var createdAuthor = (Author)_scenarioContext[ScenarioContextStorage.CreatedAuthor.ToString()];
            MyDbConnector.GetConnection().Execute(SqlQuery.DeleteAuthorById(createdAuthor.id));
        }
    }
}

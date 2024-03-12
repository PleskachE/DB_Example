using NUnit.Framework;
using TechTalk.SpecFlow;
using System.Linq;
using DB_Example.Models.DbModel;
using DB_Example.SqlQueries;
using DB_Example.Enums;
using DB_Example.Utils;
using System.Collections.Generic;
using DB_Example.Constants;

namespace DB_Example.Steps
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
            MyDbExecuter.GetInstance().ExecuteCommandInput(SqlQuery.InsertNewAuthor(author.name, author.login, author.email));
            _scenarioContext.Add(ScenarioContextStorage.CreatedAuthor.ToString(), author);
        }

        [Then(@"Created author '(is|is not)' exist in database")]
        public void CheckAuthorExistDataBase(bool isExist)
        {
            var createdAuthor = (Author)_scenarioContext[ScenarioContextStorage.CreatedAuthor.ToString()];
            var text = MyDbExecuter.GetInstance().ExecuteCommandRead(SqlQuery.SelectAuthorByEmail(createdAuthor.email));
            var authors = new List<Author>();
            foreach (var item in text.Split(TextConstants.RowSeparator))
            {
                if (!string.IsNullOrEmpty(item))
                {
                    var args = item.Split(TextConstants.ColumnSeparator);
                    authors.Add(new Author(id: int.Parse(args[0]), name: args[1], login: args[2], email: args[3]));
                }
            }

            Assert.AreEqual(isExist, authors.Count != 0,
                $"Author <{createdAuthor.name}> is{(isExist ? " not" : string.Empty)} exist in database!");
            if (isExist)
            {
                _scenarioContext[ScenarioContextStorage.CreatedAuthor.ToString()] = (Author)authors.First();
            }
        }

        [When(@"Update property '(.*)' of created author in database as a '(.*)'")]
        public void UpdateAuthorFieldInDatabase(string propertyName, string text)
        {
            var createdAuthor = (Author)_scenarioContext[ScenarioContextStorage.CreatedAuthor.ToString()];
            MyDbExecuter.GetInstance().ExecuteCommandInput(SqlQuery.UpdateAuthorById(propertyName, text, createdAuthor.id));
            var responce = MyDbExecuter.GetInstance().ExecuteCommandRead(SqlQuery.SelectAuthorByEmail(createdAuthor.email));
            var authors = new List<Author>();
            foreach (var item in responce.Split(TextConstants.RowSeparator))
            {
                if (!string.IsNullOrEmpty(item))
                {
                    var args = item.Split(TextConstants.ColumnSeparator);
                    authors.Add(new Author(id: int.Parse(args[0]), name: args[1], login: args[2], email: args[3]));
                }
            }
            createdAuthor = authors.First();
            _scenarioContext[ScenarioContextStorage.CreatedAuthor.ToString()] = createdAuthor;
        }

        [Then(@"Property '(.*)' of created author in database '(is|is not)' equal to '(.*)'")]
        public void CheckValueOfPropertyCreatedAuthor(string propertyName, bool isEqual, string value)
        {
            var property = typeof(Author).GetProperty(propertyName);
            var createdAuthor = (Author)_scenarioContext[ScenarioContextStorage.CreatedAuthor.ToString()];
            var responce = MyDbExecuter.GetInstance().ExecuteCommandRead(SqlQuery.SelectAuthorByEmail(createdAuthor.email));
            var authors = new List<Author>();
            foreach (var item in responce.Split(TextConstants.RowSeparator))
            {
                if (!string.IsNullOrEmpty(item))
                {
                    var args = item.Split(TextConstants.ColumnSeparator);
                    authors.Add(new Author(id: int.Parse(args[0]), name: args[1], login: args[2], email: args[3]));
                }
            }
            var author = authors.First();
            var valueOfProperty = property.GetValue(author);
            Assert.AreEqual(isEqual, valueOfProperty.Equals(value),
               $"Property <{propertyName}> of created author in database <{createdAuthor.name}> is{(isEqual ? " not" : string.Empty)} equal to {value}!");
        }

        [When(@"Delete created author from database")]
        public void DeleteCreatedAuthor()
        {
            var createdAuthor = (Author)_scenarioContext[ScenarioContextStorage.CreatedAuthor.ToString()];
            MyDbExecuter.GetInstance().ExecuteCommandInput(SqlQuery.DeleteAuthorById(createdAuthor.id));
        }
    }
}

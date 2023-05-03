using EntityFrameworkExample.Enums;
using EntityFrameworkExample.Models.DbModels;
using EntityFrameworkExample.Utils;
using NUnit.Framework;
using System.Linq;
using TechTalk.SpecFlow;

namespace EntityFrameworkExample.Steps
{
    [Binding]
    public class DbSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly MyDbContext _dbContext;

        public DbSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _dbContext = new MyDbContext();
        }

        [When(@"Create and add to database new author")]
        public void CreateNewAuthor(Author author)
        {
            _dbContext.Author.Add(author);
            _dbContext.SaveChanges();
            _scenarioContext.Add(ScenarioContextStorage.CreatedAuthor.ToString(), author);
        }

        [Then(@"Created author '(is|is not)' exist in database")]
        public void CheckAuthorExistDataBase(bool isExist)
        {
            var createdAuthor = (Author)_scenarioContext[ScenarioContextStorage.CreatedAuthor.ToString()];
            Assert.AreEqual(isExist, _dbContext.Author.ToList().FirstOrDefault(x => x.Email == createdAuthor.Email) != null,
                $"Author <{createdAuthor.Name}> is{(isExist ? " not" : string.Empty)} exist in database!");
            _scenarioContext[ScenarioContextStorage.CreatedAuthor.ToString()] = _dbContext.Author.ToList().FirstOrDefault(x => x.Email == createdAuthor.Email);
        }

        [When(@"Update property '(.*)' of created author in database as a '(.*)'")]
        public void UpdateAuthorFieldInDatabase(string propertyName, string text)
        {
            var property = typeof(Author).GetProperty(propertyName);
            var createdAuthor = (Author)_scenarioContext[ScenarioContextStorage.CreatedAuthor.ToString()];
            property.SetValue(_dbContext.Author.ToList().FirstOrDefault(x => x.Email == createdAuthor.Email), text);
            _dbContext.SaveChanges();
            _scenarioContext[ScenarioContextStorage.CreatedAuthor.ToString()] = _dbContext.Author.ToList().FirstOrDefault(x => x.Id == createdAuthor.Id);
        }

        [Then(@"Property '(.*)' of created author in database '(is|is not)' equal to '(.*)'")]
        public void CheckValueOfPropertyCreatedAuthor(string propertyName, bool isEqual, string value)
        {
            var property = typeof(Author).GetProperty(propertyName);
            var createdAuthor = (Author)_scenarioContext[ScenarioContextStorage.CreatedAuthor.ToString()];
            var valueOfProperty = property.GetValue(_dbContext.Author.ToList().FirstOrDefault(x => x.Id == createdAuthor.Id));
            Assert.AreEqual(isEqual, valueOfProperty.Equals(value),
               $"Property <{propertyName}> of created author in database <{createdAuthor.Name}> is{(isEqual ? " not" : string.Empty)} equal to {value}!");
        }

        [When(@"Delete created author from database")]
        public void DeleteCreatedAuthor()
        {
            var createdAuthor = (Author)_scenarioContext[ScenarioContextStorage.CreatedAuthor.ToString()];
            _dbContext.Author.Remove(_dbContext.Author.FirstOrDefault(x => x.Id == createdAuthor.Id));
            _dbContext.SaveChanges();
        }
    }
}

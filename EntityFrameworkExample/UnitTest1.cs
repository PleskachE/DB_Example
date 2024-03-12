using EntityFrameworkExample.Utils;
using NUnit.Framework;
using System.Linq;

namespace EntityFrameworkExample
{
    public class Tests
    {
        [Test]
        public void TestOfEntityFramework()
        {
            var dbContext = new MyDbContext();
            var author = dbContext.Author.FirstOrDefault(item => item.Name == "engineer3");
            var tests = dbContext.Test.ToList().FindAll(item => item.AuthorId == author.Id);
        }
    }
}
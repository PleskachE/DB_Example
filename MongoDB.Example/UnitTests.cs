using MongoDB.Driver;
using MongoDB.Example.Models;
using MongoDB.Example.Utils;
using NUnit.Framework;

namespace MongoDB.Example
{
    public class UnitTests
    {
        [Test]
        public void Test1()
        {
            //Read
            IMongoCollection<User> users = MongoDbConnector.Instance.Database.GetCollection<User>("users");

            //Create
            var newUser = new User()
            {
                Name = "John Doe",
                Age = 30
            };
            users.InsertOne(newUser);

            //Update
            var filter = Builders<User>.Filter.Eq("Name", "John Doe");
            var update = Builders<User>.Update.Set("Age", 31);
            UpdateResult updateResult = users.UpdateOne(filter, update);

            //Delete
            filter = Builders<User>.Filter.Eq("Name", "John Doe");
            DeleteResult deleteResult = users.DeleteOne(filter);
        }
    }
}

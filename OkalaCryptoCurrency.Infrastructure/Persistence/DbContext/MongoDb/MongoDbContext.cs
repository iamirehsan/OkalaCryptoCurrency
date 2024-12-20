using MongoDB.Driver;

namespace OkalaCryptoCurrency.Infrastructure.Persistence.DbContext.MongoDb
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext()
        {
            var client = new MongoClient(Environment.GetEnvironmentVariable("MONGO_URI"));
            _database = client.GetDatabase(Environment.GetEnvironmentVariable("DB_Name"));

        }
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
}

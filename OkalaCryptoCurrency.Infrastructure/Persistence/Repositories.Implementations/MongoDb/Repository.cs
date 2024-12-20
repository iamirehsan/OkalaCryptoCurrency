using MongoDB.Driver;
using OkalaCryptoCurrency.Domain.Interfaces.Repositories.MongoDb;
using OkalaCryptoCurrency.Infrastructure.Persistence.DbContext.MongoDb;

namespace OkalaCryptoCurrency.Infrastructure.Persistence.Repositories.Implementations.MongoDb
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public Repository(MongoDbContext context, string collectionName)
        {
            _collection = context.GetCollection<T>(collectionName);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(Builders<T>.Filter.Empty).ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _collection.Find(Builders<T>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
        }
        public async Task<T> GetByColumnName<ColumnType>(ColumnType columnTypeValue, string columnName)
        {
            return await _collection.Find(Builders<T>.Filter.Eq(columnName, columnTypeValue)).FirstOrDefaultAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            var id = (Guid)typeof(T).GetProperty("Id").GetValue(entity);
            await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("Id", id), entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));
        }
    }
}

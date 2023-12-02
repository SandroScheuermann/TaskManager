using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using TaskManager.Application.ConfigurationModels;
using TaskManager.Domain.Entities.Shared;
using TaskManager.Domain.Repositories;

namespace Muscler.Infra.DataAccess.Shared
{
    public class MongoRepository<T> : IMongoRepository<T> where T : MongoEntity
    {
        protected IMongoCollection<T> Collection { get; set; }

        public MongoRepository(IOptions<DefaultSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            Collection = mongoDatabase.GetCollection<T>(typeof(T).Name);
        }

        public async Task InsertAsync(T item)
        {
            await Collection.InsertOneAsync(item);
        }

        public async Task<DeleteResult> DeleteAsync(string id)
        {
            var deleteByIdFilter = Builders<T>.Filter.Eq(entity => entity.Id, id);

            return await Collection.DeleteOneAsync(deleteByIdFilter);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var filter = Builders<T>.Filter.Empty;
            return await Collection.Find(filter).ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var getByIdFilter = Builders<T>.Filter.Eq(entity => entity.Id, id);

            return await Collection.Find(getByIdFilter).FirstOrDefaultAsync();
        }

        public async Task<bool> CheckExistanceById(string id)
        {
            var getByIdFilter = Builders<T>.Filter.Eq(entity => entity.Id, id);

            var countResult = await Collection.CountDocumentsAsync(getByIdFilter); 

            return await Collection.CountDocumentsAsync(getByIdFilter) > 0;
        }  

        public async Task<UpdateResult> UpdateAsync(T item)
        {  
            var filter = Builders<T>.Filter.Eq("Id", item.Id);

            var updateDefinition = new List<UpdateDefinition<T>>();

            foreach (var property in typeof(T).GetProperties())
            { 
                if (property.Name != "Id")
                {
                    var value = property.GetValue(item);
                    if (value != null) 
                    {
                        updateDefinition.Add(Builders<T>.Update.Set(property.Name, value));
                    }
                }
            }

            var update = Builders<T>.Update.Combine(updateDefinition);

            return await Collection.UpdateOneAsync(filter, update);
        }
    }
}

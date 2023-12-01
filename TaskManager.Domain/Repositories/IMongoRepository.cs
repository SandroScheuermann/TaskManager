using MongoDB.Driver;
using TaskManager.Domain.Entities.Shared;

namespace TaskManager.Domain.Repositories
{
    public interface IMongoRepository<T> where T : MongoEntity
    {
        public Task<List<T>> GetAllAsync();

        public Task<T> GetByIdAsync(string id);

        public Task InsertAsync(T item);

        public Task<DeleteResult> DeleteAsync(string id);

        public Task<UpdateResult> UpdateAsync(T item);

        public Task<bool> CheckIfExistsById(string id);
    }
}

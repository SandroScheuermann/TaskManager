using MongoDB.Driver;
using TaskManager.Domain.Entities.Shared;

namespace TaskManager.Domain.Repositories.Shared
{
    public interface IMongoRepository<T> where T : MongoEntity
    {
        public Task<IEnumerable<T>> GetAllAsync();

        public Task<T> GetByIdAsync(string id);

        public Task InsertAsync(T item);

        public Task InserManyAsync(IEnumerable<T> items);

        public Task<DeleteResult> DeleteAsync(string id);

        public Task<UpdateResult> UpdateAsync(T item);

        public Task<bool> CheckExistanceById(string id);
    }
}

using MongoDB.Driver;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Services.UserService
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(User user);
        Task<User> GetUserByIdAsync(string id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<ReplaceOneResult> UpdateUserAsync(User user);
        Task<DeleteResult> DeleteUserAsync(string id); 
    }
}

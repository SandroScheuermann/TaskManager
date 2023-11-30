using MongoDB.Driver;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Services.UserService
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public IUserRepository UserRepository { get; set; } = userRepository;

        public async Task<User> CreateUserAsync(User user)
        {
            await UserRepository.InsertAsync(user);

            return user;
        }

        public async Task<DeleteResult> DeleteUserAsync(string id)
        {
            return await UserRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await UserRepository.GetAllAsync();
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await UserRepository.GetByIdAsync(id);
        }

        public async Task<ReplaceOneResult> UpdateUserAsync(User user)
        {
            return await UserRepository.UpdateAsync(user);
        }
    }
}

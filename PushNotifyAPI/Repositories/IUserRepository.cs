using PushNotifyAPI.Entities;

namespace PushNotifyAPI.Repositories
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAsync();
        public Task<User?> GetAsync(string userId);
        public Task CreateAsync(User newUser);

        public Task UpdateAsync(string id, User updatedUser);

        public Task RemoveAsync(string id);
    }
}

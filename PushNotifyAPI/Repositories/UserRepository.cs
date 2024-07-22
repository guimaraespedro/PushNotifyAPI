using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PushNotifyAPI.Entities;

namespace PushNotifyAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(DatabaseCore databaseCore)
        {
            _users = databaseCore.database.GetCollection<User>("User");
        }

        public async Task<List<User>> GetAsync() =>
            await _users.Find(_ => true).ToListAsync();

        public async Task<User?> GetAsync(string id) =>
            await _users.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(User newUser) =>
            await _users.InsertOneAsync(newUser);

        public async Task UpdateAsync(string id, User updatedUser) =>
            await _users.ReplaceOneAsync(x => x.Id == id, updatedUser);

        public async Task RemoveAsync(string id) =>
            await _users.DeleteOneAsync(x => x.Id == id);
    }
}

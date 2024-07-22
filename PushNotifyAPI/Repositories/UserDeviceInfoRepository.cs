using MongoDB.Driver;
using PushNotifyAPI.Entities;

namespace PushNotifyAPI.Repositories
{
    public class UserDeviceInfoRepository : IUserDeviceInfoRepository
    {
        private readonly IMongoCollection<UserDeviceInfo> _notifications;

        public UserDeviceInfoRepository(DatabaseCore databaseCore)
        {
            _notifications = databaseCore.database.GetCollection<UserDeviceInfo>("UserDeviceInfo");
        }

        public async Task<List<UserDeviceInfo>> GetAsync() =>
            await _notifications.Find(_ => true).ToListAsync();

        public async Task<UserDeviceInfo?> GetAsync(string deviceToken)
        {
            return await _notifications.Find(x => x.DeviceToken == deviceToken).FirstOrDefaultAsync();
        }

        public async Task<UserDeviceInfo?> GetAsyncFromUserId(string userId)
        {
            return await _notifications.Find(x => x.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(UserDeviceInfo userDeviceInfo) =>
            await _notifications.InsertOneAsync(userDeviceInfo);

        public async Task RemoveAsync(string deviceToken) =>
            await _notifications.DeleteOneAsync(x => x.DeviceToken == deviceToken);
    }
}

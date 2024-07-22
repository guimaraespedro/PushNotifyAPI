using PushNotifyAPI.Entities;

namespace PushNotifyAPI.Repositories
{
    public interface IUserDeviceInfoRepository
    {
        public Task<List<UserDeviceInfo>> GetAsync();
        public Task<UserDeviceInfo?> GetAsync(string deviceToken);
        public  Task<UserDeviceInfo?> GetAsyncFromUserId(string userId); 
        public Task CreateAsync(UserDeviceInfo newUserDeviceInfo);
        public Task RemoveAsync(string deviceToken);
    }
}

using PushNotifyAPI.Entities;

namespace PushNotifyAPI.Repositories
{
    public interface INotificationRepository
    {
        public Task<List<Notification>> GetAsync(string userId);
        public Task CreateAsync(Notification newUser);
        public Task RemoveAsync(string id);
    }
}

using MongoDB.Driver;
using PushNotifyAPI.Entities;

namespace PushNotifyAPI.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IMongoCollection<Notification> _notifications;

        public NotificationRepository(DatabaseCore databaseCore)
        {
            _notifications = databaseCore.database.GetCollection<Notification>("Notification");
        }

        public async Task<List<Notification>> GetAsync(string id)
        {
            return await _notifications.Find(x => x.Id.ToString() == id).ToListAsync();
        }

        public async Task CreateAsync(Notification notification) =>
            await _notifications.InsertOneAsync(notification);

        public async Task RemoveAsync(string id) =>
            await _notifications.DeleteOneAsync(x => x.Id.ToString() == id);
    }
}

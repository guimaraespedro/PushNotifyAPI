

using FirebaseAdmin.Messaging;
using PushNotifyAPI.DTOS;

namespace PushNotifyAPI.Services
{
    public class PushService : IPushService
    {
        private FirebaseMessaging _messaging;
        public PushService() {
            _messaging = FirebaseMessaging.DefaultInstance;
        }

        public async Task SendNotificationAsync(string receiverDeviceToken, NotificationDTO notificationDTO)
        {
            Message message = new()
            {
                Notification = new Notification
                {
                    Title = notificationDTO.Title,
                    Body = notificationDTO.Content,
                },
                Token = receiverDeviceToken

            };

            await _messaging.SendAsync(message);
        }
    }
}

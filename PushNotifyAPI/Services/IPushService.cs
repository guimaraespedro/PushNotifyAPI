using PushNotifyAPI.DTOS;

namespace PushNotifyAPI.Services
{
    public interface IPushService
    {
        Task SendNotificationAsync(string receiver, NotificationDTO notification);
    }
}

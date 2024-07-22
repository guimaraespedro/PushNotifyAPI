namespace PushNotifyAPI.DTOS
{
    public class NotificationDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string ReceiverUserId { get; set; } = string.Empty;
        public string SenderUserId { get; set; } = string.Empty;

    }
}

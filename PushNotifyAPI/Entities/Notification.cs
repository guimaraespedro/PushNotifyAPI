using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PushNotifyAPI.Entities
{
    public class Notification
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string ReceiverId { get; set; } = string.Empty;
        public string ReceiverDeviceToken { get; set; } = string.Empty;
        public string SenderId { get; set; } = string.Empty;
        public DateTime SendAt { get; set; }

    }
}

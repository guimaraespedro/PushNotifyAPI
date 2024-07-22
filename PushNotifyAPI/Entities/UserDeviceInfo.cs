using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PushNotifyAPI.Entities
{
    public class UserDeviceInfo
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string DeviceToken { get; set; } = string.Empty;
        [BsonRepresentation(BsonType.String)]
        public string UserId { get; set; } = string.Empty;

    }
}

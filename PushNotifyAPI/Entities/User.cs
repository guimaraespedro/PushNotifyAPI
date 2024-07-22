using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PushNotifyAPI.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; } = string.Empty;
        [BsonRepresentation(BsonType.String)]
        public string Email { get; set; } = string.Empty;
        [BsonRepresentation(BsonType.String)]
        public string Name { get; set; } = string.Empty;

    }
}

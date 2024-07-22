using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PushNotifyAPI.Database;
using System.Security.Authentication;

namespace PushNotifyAPI
{
    public class DatabaseCore
    {
        public IMongoDatabase database;
        public IConfiguration _config;
        public DatabaseCore(IOptions<DatabaseSettings> databaseSettings, IConfiguration config)
        {
            var connectionStrings = config.GetConnectionString("Default");
            MongoClientSettings settings = MongoClientSettings.FromUrl(
              new MongoUrl(connectionStrings)
            );
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);
            database = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _config = config;
        }

    }
}

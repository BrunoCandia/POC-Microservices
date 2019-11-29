using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Users.API.Infrastructure.Mongo
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase Database { get; set; }

        private MongoClient MongoClient { get; set; }

        //private readonly IConfiguration _configuration;

        private readonly IOptions<UserSettings> _settings;

        public MongoContext(IOptions<UserSettings> settings)
        {
            //_configuration = configuration;
            _settings = settings;
        }

        private void ConfigureMongo()
        {
            if (MongoClient != null)
                return;

            // Configure mongo (You can inject the config, just to simplify)
            //MongoClient = new MongoClient(_configuration["MongoSettings:Connection"]);
            //MongoClient = new MongoClient(_configuration["ConnectionString"]);

            MongoClient = new MongoClient(_settings.Value.ConnectionString);

            //Database = MongoClient.GetDatabase(_configuration["MongoSettings:DatabaseName"]);
            //Database = MongoClient.GetDatabase(_configuration["Database"]);

            Database = MongoClient.GetDatabase(_settings.Value.Database);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            ConfigureMongo();
            return Database.GetCollection<T>(name);
        }

        public void Dispose()
        {            
            GC.SuppressFinalize(this);
        }
    }
}

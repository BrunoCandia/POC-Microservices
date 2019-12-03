using Microsoft.Extensions.Options;
using MongoDB.Driver;
using usersModel = Users.API.Model;

namespace Users.API.Infrastructure
{
    public class UsersContext
    {
        private readonly IMongoDatabase _database = null;

        public UsersContext(IOptions<UserSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);

            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<usersModel.UsersModel> Users
        {
            get
            {
                return _database.GetCollection<usersModel.UsersModel>("UsersModel");
            }
        }
    }
}

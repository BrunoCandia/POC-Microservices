using Microsoft.Extensions.Options;
using MongoDB.Driver;
using usersModel = Users.API.Model;

namespace Users.API.Infrastructure
{
    public class UsersContext //: IUsersContext
    {
        private const string usersCollectionName = "Users";
        private readonly IMongoDatabase _database = null;
        private IMongoCollection<usersModel.UsersModel> users;

        public UsersContext(IOptions<UserSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);

            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);            
        }

        public UsersContext(IMongoDatabase _database)
        {            
            //this._database = _database;
        }

        public IMongoCollection<usersModel.UsersModel> Users
        {
            get
            {
                if (users is null)
                { 
                    users = _database.GetCollection<usersModel.UsersModel>(usersCollectionName); 
                }

                return users;
            }
        }
    }
}

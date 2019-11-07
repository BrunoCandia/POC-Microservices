using MongoDB.Driver;
using usersModel = Users.API.Model;

namespace Users.API.Infrastructure
{
    public interface IUsersContext
    {
        IMongoCollection<usersModel.UsersModel> Users { get; }
    }
}

using System;
using MongoDB.Driver;
using usersModel = Users.API.Model;

namespace Users.API.Infrastructure
{
    public interface IUsersContext : IDisposable
    {
        IMongoCollection<usersModel.UsersModel> Users { get; }
    }
}

using System;
using MongoDB.Driver;

namespace Users.API.Infrastructure.Mongo
{
    public interface IMongoContext : IDisposable
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}

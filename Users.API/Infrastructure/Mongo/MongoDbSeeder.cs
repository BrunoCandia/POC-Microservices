using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Users.API.Infrastructure.Mongo
{
    public class MongoDbSeeder : IMongoDbSeeder
    {
        protected readonly IMongoDatabase Database;

        public MongoDbSeeder(IMongoDatabase database)
        {
            Database = database;
        }

        public async Task SeedAsync()
        {
            await CustomSeedAsync();
        }

        protected virtual async Task CustomSeedAsync()
        {
            var cursor = await Database.ListCollectionsAsync();
            var collections = await cursor.ToListAsync();

            if (collections.Any())
            {
                return;
            }

            await Task.CompletedTask;
        }

        //private async Task SetUsersAsync()
        //{
        //    var userList = new List<UsersModel>{
        //        new UsersModel { UserId = 1, FirstName = "Juan", LastName = "Perez" },
        //        new UsersModel { UserId = 2, FirstName = "Pepe", LastName = "Lopez" },
        //        new UsersModel { UserId = 3, FirstName = "Ramon", LastName = "Diaz" }
        //    };

        //    await Database.Users.InsertManyAsync(userList);
        //}
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.API.Model;

namespace Users.API.Infrastructure
{
    public class UsersContextSeed
    {
        private static UsersContext ctx;

        public static async Task SeedAsync(IApplicationBuilder applicationBuilder, ILoggerFactory loggerFactory)
        {
            var config = applicationBuilder.ApplicationServices.GetRequiredService<IOptions<UserSettings>>();

            ctx = new UsersContext(config);

            if (!ctx.Users.Database.GetCollection<UsersModel>(nameof(UsersModel)).AsQueryable().Any())
            {
                await SetIndexesAsync();
                await SetUsersAsync();
            }

            //if (await ctx.Users.Database.GetCollection<UsersModel>(nameof(UsersModel)).CountDocumentsAsync(new BsonDocument()) == 0)
            //{
            //    await SetIndexesAsync();
            //    await SetUsersAsync();
            //}

            //var filter = Builders<UsersModel>.Filter.Empty;
            //var usersModel = ctx.Users.Database.GetCollection<UsersModel>(nameof(UsersModel)).AsQueryable().ToList();
            //var count = ctx.Users.Database.GetCollection<UsersModel>(nameof(UsersModel)).Find(filter).ToList().Count();

            //if (ctx.Users.Database.GetCollection<UsersModel>(nameof(UsersModel)).Find(filter).ToList().Count() == 0)
            //{
            //    await SetIndexesAsync();
            //    await SetUsersAsync();
            //}
        }

        private static async Task SetIndexesAsync()
        {
            // Set location indexes
            var builder = Builders<UsersModel>.IndexKeys;            
            var indexModel = new CreateIndexModel<UsersModel>(builder.Ascending(x => x.LastName));
            await ctx.Users.Indexes.CreateOneAsync(indexModel);
        }

        private static async Task SetUsersAsync()
        {
            var userList = new List<UsersModel>{
                new UsersModel { UserId = 1, FirstName = "Juan", LastName = "Perez" },
                new UsersModel { UserId = 2, FirstName = "Pepe", LastName = "Lopez" },
                new UsersModel { UserId = 3, FirstName = "Ramon", LastName = "Diaz" }
            };
            
            await ctx.Users.InsertManyAsync(userList);
        }
    }
}

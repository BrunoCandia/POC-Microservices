using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.API.Infrastructure.Mongo;
using Users.API.Model;

namespace Users.API.Infrastructure
{
    public class UsersContextSeed
    {        
        private readonly IMongoContext _context;

        public UsersContextSeed(IMongoContext context)
        {
            _context = context;
        }

        public async Task SeedAsync(IApplicationBuilder applicationBuilder, ILoggerFactory loggerFactory)
        {            
            if (!_context.GetCollection<UsersModel>("UsersModel").AsQueryable().Any())
            {
                if (!_context.GetCollection<UsersModel>("UsersModel").Find(new BsonDocument()).ToList().Any())
                {
                    await SetIndexesAsync();
                    await SetUsersAsync();
                }                
            }            
        }        

        private async Task SetIndexesAsync()
        {
            // Set location indexes
            var builder = Builders<UsersModel>.IndexKeys;
            var indexModel = new CreateIndexModel<UsersModel>(builder.Ascending(x => x.LastName));            
            await _context.GetCollection<UsersModel>("UsersModel").Indexes.CreateOneAsync(indexModel);
        }

        private async Task SetUsersAsync()
        {
            var userList = new List<UsersModel>{
                new UsersModel { UserId = 1, FirstName = "Juan", LastName = "Perez" },
                new UsersModel { UserId = 2, FirstName = "Pepe", LastName = "Lopez" },
                new UsersModel { UserId = 3, FirstName = "Ramon", LastName = "Diaz" }
            };

            await _context.GetCollection<UsersModel>("UsersModel").InsertManyAsync(userList);
        }
    }
}

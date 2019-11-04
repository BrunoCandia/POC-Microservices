using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.API.Model;

namespace Users.API.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UsersContext _context;

        public UsersRepository(IOptions<UserSettings> settings)
        {
            _context = new UsersContext(settings);
        }

        public async Task<List<UsersModel>> GetUserListAsync()
        {
            return await _context.Users.Find(new BsonDocument()).ToListAsync();
            
            //return await GetMockedData();
        }

        public async Task<UsersModel> GetUserAsync(int userId)
        {
            var filter = Builders<UsersModel>.Filter.Eq("UserId", userId);

            return await _context.Users
                                 .Find(filter)
                                 .FirstOrDefaultAsync();
        }

        private Task<List<UsersModel>> GetMockedData()
        {            
            return Task.Run(() => {
                var usersList = new List<UsersModel> {
                    new UsersModel { /*Id = "1", */FirstName = "Juan", LastName = "Perez" },
                    new UsersModel { /*Id = "2", */FirstName = "Pepe", LastName = "Lopez" },
                    new UsersModel { /*Id = "3", */FirstName = "Ramon", LastName = "Diaz" }
                };

                return usersList;
            });
        }
    }
}

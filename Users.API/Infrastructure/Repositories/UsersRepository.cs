using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.API.Infrastructure.Mongo;
using Users.API.Model;

namespace Users.API.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UsersContext _context;
        private readonly IMongoRepository<UsersModel> _repository;

        public UsersRepository(IOptions<UserSettings> settings)
        {
            _context = new UsersContext(settings);
        }

        public UsersRepository(IMongoRepository<UsersModel> repository)
        {
            _repository = repository;
        }

        public async Task<List<UsersModel>> GetUserListAsync()
        {
            return await _context.Users.Find(new BsonDocument()).ToListAsync();                        
        }

        public async Task<UsersModel> GetUserAsync(int userId)
        {
            var filter = Builders<UsersModel>.Filter.Eq("UserId", userId);

            return await _context.Users
                                 .Find(filter)
                                 .FirstOrDefaultAsync();
        }                

        public async Task<UsersModel> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task<bool> ExistsAsync(Guid id)
            => await _repository.ExistsAsync(p => p.Id == id);

        public async Task<bool> ExistsAsync(string firstName)
            => await _repository.ExistsAsync(p => p.FirstName == firstName.ToLowerInvariant());

        //public async Task<PagedResult<UsersModel>> BrowseAsync(BrowseProducts query)
        //    => await _repository.BrowseAsync(p =>
        //        p.Price >= query.PriceFrom && p.Price <= query.PriceTo, query);

        public async Task AddAsync(UsersModel product)
            => await _repository.AddAsync(product);

        public async Task UpdateAsync(UsersModel product)
            => await _repository.UpdateAsync(product);

        public async Task DeleteAsync(Guid id)
            => await _repository.DeleteAsync(id);

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

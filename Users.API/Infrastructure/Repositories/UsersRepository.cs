using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Users.API.Infrastructure.Mongo;
using Users.API.Model;

namespace Users.API.Infrastructure.Repositories
{
    public class UsersRepository : BaseRepository<UsersModel>, IUsersRepository
    {
        private readonly IMongoContext _context;

        public UsersRepository(IMongoContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<UsersModel>> GetUserListAsync()
        {
            return await _context.GetCollection<UsersModel>("UsersModel").Find(new BsonDocument()).ToListAsync();            
        }        

        public async Task<UsersModel> GetUserAsync(int userId)
        {
            var filter = Builders<UsersModel>.Filter.Eq("UserId", userId);

            return await _context.GetCollection<UsersModel>("UsersModel")
                                 .Find(filter)
                                 .FirstOrDefaultAsync();
        }
    }
}

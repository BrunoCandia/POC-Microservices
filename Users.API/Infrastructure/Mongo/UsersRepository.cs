using Users.API.Model;

namespace Users.API.Infrastructure.Mongo
{
    public class UsersRepository : BaseRepository<UsersModel>, IUsersRepository
    {
        public UsersRepository(IMongoContext context) : base(context)
        {
        }
    }
}

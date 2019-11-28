using Users.API.Model;

namespace Users.API.Infrastructure.Mongo
{
    public interface IUsersRepository : IRepository<UsersModel>
    {
    }
}

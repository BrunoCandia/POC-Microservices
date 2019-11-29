using System.Collections.Generic;
using System.Threading.Tasks;
using Users.API.Model;

namespace Users.API.Infrastructure.Repositories
{    
    public interface IUsersRepository : IRepository<UsersModel>
    {
        Task<List<UsersModel>> GetUserListAsync();

        Task<UsersModel> GetUserAsync(int userId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.API.Model;

namespace Users.API.Infrastructure.Repositories
{
    public interface IUsersRepository
    {
        Task<List<UsersModel>> GetUserListAsync();

        Task<UsersModel> GetUserAsync(int userId);
    }
}

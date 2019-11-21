using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.API.Model;

namespace Users.API.Infrastructure.Services
{
    public interface IUsersService
    {
        //Task<List<UsersModel>> GetAllUserAsync();

        //Task<UsersModel> GetUserAsync(int userId);

        Task<List<UsersModel>> GetAllAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.API.Model;

namespace Users.API.Infrastructure.Repositories
{
    public interface IUsersRepository
    {
        //Task<List<UsersModel>> GetUserListAsync();

        //Task<UsersModel> GetUserAsync(int userId);

        Task<List<UsersModel>> GetAllAsync();

        Task<UsersModel> GetAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> ExistsAsync(string name);
        //Task<PagedResult<UsersModel>> BrowseAsync(BrowseProducts query);
        Task AddAsync(UsersModel product);
        Task UpdateAsync(UsersModel product);
        Task DeleteAsync(Guid id);
    }
}

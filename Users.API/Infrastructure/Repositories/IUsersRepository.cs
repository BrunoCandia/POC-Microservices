using System.Collections.Generic;
using System.Threading.Tasks;
using Users.API.Model;

namespace Users.API.Infrastructure.Repositories
{    
    public interface IUsersRepository : IRepository<UsersModel, MongoDB.Bson.ObjectId>
    {
        Task<List<UsersModel>> GetUserListAsync();

        Task<UsersModel> GetUserAsync(int userId);

        MongoDB.Driver.FilterDefinition<T> GetFilters<T>(
            IDictionary<string, string> fieldEqValue = null,
            IDictionary<string, string> fieldContainsValue = null,
            IDictionary<string, IEnumerable<string>> fieldEqValues = null,
            IDictionary<string, IEnumerable<string>> fieldElemMatchInValues = null,
            IEnumerable<MongoDB.Bson.ObjectId> ids = null);
    }
}

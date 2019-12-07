using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Users.API.DTO.Common.Paging.Request;
using Users.API.DTO.Paging.Response;
using Users.API.Infrastructure.Repositories;
using Users.API.Model;

namespace Users.API.Infrastructure.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
        }

        public async Task<List<UsersModel>> GetAllUserAsync()
        {
            return await _usersRepository.GetUserListAsync();
        }

        public async Task<UsersModel> GetUserAsync(int userId)
        {
            return await _usersRepository.GetUserAsync(userId);
        }

        public async Task<IEnumerable<UsersModel>> GetAllAsync()
        {
            return await _usersRepository.GetAllAsync();
        }

        public async Task<IPagedResult<UsersModel>> GetPagedAsync(PagedRequestDTO requestPaged, string requestFilter)
        {
            var filter = Builders<UsersModel>.Filter.Eq("LastName", requestFilter);
            
            return await _usersRepository.GetPagedListAsync(requestPaged, filter);
        }

        public async Task<IPagedResult<UsersModel>> GetPagedAsync(PagedRequestDTO requestPaged, Dictionary<string, string> fieldsValues = null, DTO.Common.SortDTO sortData = null)
        {            
            var filters = _usersRepository.GetFilters<UsersModel>(fieldsValues);
            var sort = _usersRepository.GetSortDirection<UsersModel>(sortData);

            return await _usersRepository.GetPagedListAsync(requestPaged, filters, sort);
        }
    }
}

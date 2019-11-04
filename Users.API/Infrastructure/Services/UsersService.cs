using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    }
}

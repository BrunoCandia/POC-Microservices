using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Users.API.Infrastructure.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task AddAsync(TEntity obj);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task UpdateAsync(TEntity obj);
        Task RemoveAsync(Guid id);
    }
}

using System;
using System.Threading.Tasks;

namespace Users.API.Infrastructure.Mongo
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}

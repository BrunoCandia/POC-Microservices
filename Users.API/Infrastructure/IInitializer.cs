using System.Threading.Tasks;

namespace Users.API.Infrastructure
{
    public interface IInitializer
    {
        Task InitializeAsync();
    }
}

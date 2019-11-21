using System.Threading.Tasks;

namespace Users.API.Infrastructure.Mongo
{
    public interface IMongoDbSeeder
    {
        Task SeedAsync();
    }
}

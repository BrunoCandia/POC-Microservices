
namespace Users.API.Infrastructure
{
    public interface IStartupInitializer
    {
        public interface IStartupInitializer : IInitializer
        {
            void AddInitializer(IInitializer initializer);
        }
    }
}

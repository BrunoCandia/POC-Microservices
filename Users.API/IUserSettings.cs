
namespace Users.API
{
    public interface IUserSettings
    {
        //string ExternalCatalogBaseUrl { get; set; }
        //string EventBusConnection { get; set; }
        string ConnectionString { get; set; }
        string Database { get; set; }
    }
}

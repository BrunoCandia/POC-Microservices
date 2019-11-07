
namespace Users.API
{
    public class UserSettings : IUserSettings
    {
        //public string ExternalCatalogBaseUrl { get; set; }
        //public string EventBusConnection { get; set; }
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
}

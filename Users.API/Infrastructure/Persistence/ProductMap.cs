using MongoDB.Bson.Serialization;
using Users.API.Model;

namespace Users.API.Infrastructure.Persistence
{
    public class ProductMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<UsersModel>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdMember(x => x.Id);
                //map.MapMember(x => x.FirstName).SetIsRequired(true);
            });
        }
    }
}

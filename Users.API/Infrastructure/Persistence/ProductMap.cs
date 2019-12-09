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
                //map.MapIdMember(x => x.Id);                           //Id is of BaseDocument type and gives an exception
                //map.MapMember(x => x.FirstName).SetIsRequired(true);
            });
        }
    }
}

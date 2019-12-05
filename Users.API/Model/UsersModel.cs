using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Users.API.Infrastructure.Documents;

namespace Users.API.Model
{
    public class UsersModel : BaseDocument<ObjectId>
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public string Id { get; set; }

        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }
    }
}

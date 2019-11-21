using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Users.API.Infrastructure.Mongo;

namespace Users.API.Model
{
    public class UsersModel : IIdentifiable
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public string Id { get; set; }

        public Guid Id { get; protected set; }

        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }
    }
}

using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Users.API.Infrastructure.Documents
{
    public abstract class BaseDocument<TEntityKey>
    {
        protected BaseDocument()
        {
            CreatedOn = DateTime.UtcNow;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public TEntityKey Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}

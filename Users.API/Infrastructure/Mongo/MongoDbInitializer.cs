using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Users.API.Infrastructure.Mongo
{
    public class MongoDbInitializer : IMongoDbInitializer
    {
        private static bool initialized;
        private readonly bool seed;
        private readonly IMongoDatabase database;
        private readonly IMongoDbSeeder seeder;

        public MongoDbInitializer(IMongoDatabase database,
            IMongoDbSeeder seeder,
            MongoDbOptions options)
        {
            database = database;
            seeder = seeder;
            seed = options.Seed;
        }

        public async Task InitializeAsync()
        {
            if (initialized)
            {
                return;
            }

            RegisterConventions();

            initialized = true;

            if (!seed)
            {
                return;
            }

            await seeder.SeedAsync();
        }

        private void RegisterConventions()
        {
            BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
            BsonSerializer.RegisterSerializer(typeof(decimal?), new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));
            ConventionRegistry.Register("Conventions", new MongoDbConventions(), x => true);
        }

        private class MongoDbConventions : IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(BsonType.String),
                new CamelCaseElementNameConvention()
            };
        }
    }
}

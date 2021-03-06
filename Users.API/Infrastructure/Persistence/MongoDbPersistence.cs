﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;

namespace Users.API.Infrastructure.Persistence
{
    public static class MongoDbPersistence
    {
        public static void Configure()
        {
            ProductMap.Configure();

            // Set Guid to CSharp style (with dash -)
            BsonDefaults.GuidRepresentation = GuidRepresentation.CSharpLegacy;

            // Conventions
            var pack = new ConventionPack
                {
                    new CamelCaseElementNameConvention(),
                    new IgnoreExtraElementsConvention(true),
                    new IgnoreIfDefaultConvention(true)
                };

            ConventionRegistry.Register("My Solution Conventions", pack, type => true);
        }
    }
}

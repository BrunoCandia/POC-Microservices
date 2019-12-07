using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Users.API.DTO.Common;
using Users.API.DTO.Common.Paging.Request;
using Users.API.Infrastructure.Mongo;
using Users.API.Model;

namespace Users.API.Infrastructure.Repositories
{
    public class UsersRepository : BaseRepository<UsersModel, ObjectId>, IUsersRepository
    {
        private readonly IMongoContext _context;

        public UsersRepository(IMongoContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<UsersModel>> GetUserListAsync()
        {
            return await _context.GetCollection<UsersModel>("UsersModel").Find(new BsonDocument()).ToListAsync();            
        }        

        public async Task<UsersModel> GetUserAsync(int userId)
        {
            var filter = Builders<UsersModel>.Filter.Eq("UserId", userId);

            return await _context.GetCollection<UsersModel>("UsersModel")
                                 .Find(filter)
                                 .FirstOrDefaultAsync();
        }


        #region Generic Filter

        public async Task<IEnumerable<T>> DocumentsMatchEqFieldValueAsync<T>(string collectionName,
            IDictionary<string, string> fieldEqValue = null,
            IDictionary<string, string> fieldContainsValue = null,
            IDictionary<string, IEnumerable<string>> fieldEqValues = null,
            IDictionary<string, IEnumerable<string>> fieldElemMatchInValues = null,
            IEnumerable<ObjectId> ids = null)
        {
            var cursor = await GetEqAsyncCursor<T>(collectionName, fieldEqValue, fieldContainsValue, fieldEqValues, fieldElemMatchInValues, ids).ConfigureAwait(false);
            return await cursor.ToListAsync().ConfigureAwait(false);
        }

        protected Task<IAsyncCursor<T>> GetEqAsyncCursor<T>(string collectionName,
            IDictionary<string, string> fieldEqValue = null,
            IDictionary<string, string> fieldContainsValue = null,
            IDictionary<string, IEnumerable<string>> fieldEqValues = null,
            IDictionary<string, IEnumerable<string>> fieldElemMatchInValues = null,
            IEnumerable<ObjectId> ids = null)
        {
            var collection = _context.GetCollection<T>(collectionName);
            var builder = Builders<T>.Filter;

            IList<FilterDefinition<T>> filters = new List<FilterDefinition<T>>();

            if (fieldEqValue != null && fieldEqValue.Any())
            {
                filters.Add(fieldEqValue
                            .Select(p => builder.Eq(p.Key, p.Value))
                            .Aggregate((p1, p2) => p1 | p2));
            }

            if (fieldContainsValue != null && fieldContainsValue.Any())
            {
                filters.Add(fieldContainsValue
                            .Select(p => builder.Regex(p.Key, new BsonRegularExpression($".*{p.Value}.*", "i")))
                            .Aggregate((p1, p2) => p1 | p2));
            }

            if (fieldEqValues != null && fieldEqValues.Any())
            {
                foreach (var pair in fieldEqValues)
                {
                    foreach (var value in pair.Value)
                    {
                        filters.Add(builder.Eq(pair.Key, value));
                    }
                }
            }

            if (fieldElemMatchInValues != null && fieldElemMatchInValues.Any())
            {
                var baseQuery = "{ \"%key%\": { $elemMatch: { $in: [%values%] } } }";
                foreach (var item in fieldElemMatchInValues)
                {
                    var replaceKeyQuery = baseQuery.Replace("%key%", item.Key);
                    var bsonQuery = replaceKeyQuery.Replace("%values%",
                                item.Value
                                    .Select(p => $"\"{p}\"")
                                    .Aggregate((value1, value2) => $"{value1},{value2}"));

                    var filter = BsonSerializer.Deserialize<BsonDocument>(bsonQuery);
                    filters.Add(filter);
                }
            }

            if (ids != null && ids.Any())
            {
                filters.Add(ids
                        .Select(p => builder.Eq("_id", p))
                        .Aggregate((p1, p2) => p1 | p2));
            }

            var filterConcat = builder.Or(filters);

            // Here's how you can debug the generated query
            //var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            //var renderedFilter = filterConcat.Render(documentSerializer, BsonSerializer.SerializerRegistry).ToString();

            return collection.FindAsync(filterConcat);
        }

        //public async Task should_return_any_records_matching_all_possible_criteria()
        //{
        //    // Arrange
        //    IDocumentRepository documentRepository = new MongoRepository(_mongoConnectionString, _mongoDatabase);

        //    // Act
        //    var documents = await documentRepository.DocumentsMatchEqFieldValueAsync<BsonDocument>(Courses,
        //                fieldsValues: new Dictionary<string, string>
        //                {
        //                  { "state", "NJ" },
        //                  { "city", "Jersey City" }
        //                },
        //                fieldsWithEnumerableValues: new Dictionary<string, IEnumerable<string>>
        //                {
        //                  { "services", new List<string> { "Car Rental", "Locker" } },
        //                  { "amenities", new List<string> { "Sauna", "Shop" } }
        //                });

        //    // Assert
        //    documents.ShouldNotBeEmpty();
        //}

        public FilterDefinition<T> GetFilters<T>(
            IDictionary<string, string> fieldEqValue = null,
            IDictionary<string, string> fieldContainsValue = null,
            IDictionary<string, IEnumerable<string>> fieldEqValues = null,
            IDictionary<string, IEnumerable<string>> fieldElemMatchInValues = null,
            IEnumerable<ObjectId> ids = null)
        {            
            var builder = Builders<T>.Filter;

            IList<FilterDefinition<T>> filters = new List<FilterDefinition<T>>();

            if (fieldEqValue != null && fieldEqValue.Any())
            {
                filters.Add(fieldEqValue
                            .Select(p => builder.Eq(p.Key, p.Value))
                            .Aggregate((p1, p2) => p1 & p2));
            }

            if (fieldContainsValue != null && fieldContainsValue.Any())
            {
                filters.Add(fieldContainsValue
                            .Select(p => builder.Regex(p.Key, new BsonRegularExpression($".*{p.Value}.*", "i")))
                            .Aggregate((p1, p2) => p1 & p2));
            }

            if (fieldEqValues != null && fieldEqValues.Any())
            {
                foreach (var pair in fieldEqValues)
                {
                    foreach (var value in pair.Value)
                    {
                        filters.Add(builder.Eq(pair.Key, value));
                    }
                }
            }

            if (fieldElemMatchInValues != null && fieldElemMatchInValues.Any())
            {
                var baseQuery = "{ \"%key%\": { $elemMatch: { $in: [%values%] } } }";
                foreach (var item in fieldElemMatchInValues)
                {
                    var replaceKeyQuery = baseQuery.Replace("%key%", item.Key);
                    var bsonQuery = replaceKeyQuery.Replace("%values%",
                                item.Value
                                    .Select(p => $"\"{p}\"")
                                    .Aggregate((value1, value2) => $"{value1},{value2}"));

                    var filter = BsonSerializer.Deserialize<BsonDocument>(bsonQuery);
                    filters.Add(filter);
                }
            }

            if (ids != null && ids.Any())
            {
                filters.Add(ids
                        .Select(p => builder.Eq("_id", p))
                        .Aggregate((p1, p2) => p1 & p2));
            }

            FilterDefinition<T> filterConcat = null;

            if (filters.Any())
            {
                filterConcat = builder.And(filters);
            }            

            // Here's how you can debug the generated query
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();

            if (filterConcat != null)
            {
                var renderedFilter = filterConcat.Render(documentSerializer, BsonSerializer.SerializerRegistry).ToString();
            }            

            return filterConcat;
        }

        public SortDefinition<T> GetSortDirection<T>(DTO.Common.SortDTO sortData)
        {
            //Change params to DTO.Common.SortData sortData

            SortDefinition<T> sort = null;
            var builder = Builders<T>.Sort;

            if (!string.IsNullOrEmpty(sortData?.SortField))
            {
                if ((Sort)sortData?.SortDirection == Sort.Asc)
                {
                    sort = builder.Ascending(sortData.SortField);
                }
                else if ((Sort)sortData.SortDirection == Sort.Desc)
                {
                    sort = builder.Descending(sortData.SortField);
                }

            }

            return sort;
        }

        #endregion
    }
}

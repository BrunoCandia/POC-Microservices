using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Users.API.DTO.Common.Paging.Request;
using Users.API.DTO.Paging.Response;
using Users.API.Infrastructure.Documents;

namespace Users.API.Infrastructure.Extensions
{
    public static class MongoDbQueryExtensions
    {
        public static IFindFluent<TDocument, TDocument> Paged<TDocument>(this IFindFluent<TDocument, TDocument> find, PagedRequestDTO request)
        {
            return find
                .Limit(request.PageSize)
                .Skip(request.PageIndex * request.PageSize);
        }

        public static async Task<IPagedResult<TDocument>> ToPagedResultAsync<TDocument, TKey>(
            this IMongoCollection<TDocument> collection,
            FilterDefinition<TDocument> filter,
            PagedRequestDTO request,
            SortDefinition<TDocument> sort = null,
            CancellationToken cancellationToken = default(CancellationToken)) where TDocument : BaseDocument<TKey>
        {
            if (sort == null)
                sort = Builders<TDocument>.Sort.Descending(x => x.Id);

            if (filter == null)
                filter = Builders<TDocument>.Filter.Empty;

            var result = collection
                .Find(filter)
                .Sort(sort)
                .Limit(request.PageSize)
                .Skip(request.PageIndex * request.PageSize)
                .ToListAsync(cancellationToken);

            var count = collection
                .Find(filter)
                .CountDocumentsAsync(cancellationToken);

            await Task.WhenAll(result, count).ConfigureAwait(false);

            return new PagedResult<TDocument>(request.PageIndex, request.PageSize, result.Result, count.Result);
        }

        public static IPagedResult<TDocument> ToPagedResult<TDocument, TKey>(
            this IMongoCollection<TDocument> collection,
            FilterDefinition<TDocument> filter,
            PagedRequestDTO request,
            SortDefinition<TDocument> sort = null) where TDocument : BaseDocument<TKey>
        {
            if (sort == null)
                sort = Builders<TDocument>.Sort.Descending(x => x.Id);

            if (filter == null)
                filter = Builders<TDocument>.Filter.Empty;


            var result = collection
                .Find(filter)
                .Sort(sort)
                .Limit(request.PageSize)
                .Skip(request.PageIndex * request.PageSize)
                .ToList();

            var count = collection
                .Find(filter)
                .CountDocuments();

            return new PagedResult<TDocument>(request.PageIndex, request.PageSize, result, count);
        }
    }
}

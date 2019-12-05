using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Users.API.DTO.Common.Paging.Request;
using Users.API.DTO.Paging.Response;
using Users.API.Infrastructure.Documents;

namespace Users.API.Infrastructure.Repositories
{
    public interface IRepository<TEntity, TEntityKey> : IDisposable where TEntity : BaseDocument<TEntityKey>
    {
        Task AddAsync(TEntity obj);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task UpdateAsync(TEntity obj);
        Task RemoveAsync(Guid id);

        /// <summary>
        ///     Check if a document with the specified predicate exists into the store
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="cancellation">The cancellation token.</param>
        /// <returns><c>True</c> if the document exists. Otherwise <c>False</c></returns>
        Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellation = default);

        /// <summary>
        ///     Check if a document with the specified predicate exists into the store
        /// </summary>
        /// <param name="filters">The query filters.</param>
        /// <param name="cancellation">The cancellation token.</param>
        /// <returns><c>True</c> if the document exists. Otherwise <c>False</c></returns>
        Task<bool> ExistAsync(FilterDefinition<TEntity> filters, CancellationToken cancellation = default);

        /// <summary>
        ///     Check if a document with the specified id exists into the store
        /// </summary>
        /// <param name="id">The value representing the ObjectId of the entity to retrieve.</param>
        /// <param name="cancellation">The cancellation token.</param>
        /// <returns><c>True</c> if the document exists. Otherwise <c>False</c></returns>
        Task<bool> ExistAsync(TEntityKey id, CancellationToken cancellation = default);

        /// <summary>
        ///     Insert a set of documents in a single roundtrip.
        /// </summary>
        /// <param name="documents">The documents to persist.</param>
        /// <param name="cancellation">The cancellation token.</param>
        Task InsertManyAsync(IEnumerable<TEntity> documents, CancellationToken cancellation = default);

        /// <summary>
        ///     Saves the or update.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellation">The cancellation token.</param>
        /// <returns>An instance of <see cref="Task<ReplaceOneResult>"/></returns>
        Task<ReplaceOneResult> SaveOrUpdateAsync(Expression<Func<TEntity, bool>> predicate, TEntity entity, CancellationToken cancellation = default);

        /// <summary>
        ///     Saves the or update.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellation">The cancellation token.</param>
        /// <returns>An instance of <see cref="Task<ReplaceOneResult>"/></returns>
        Task<ReplaceOneResult> SaveOrUpdateAsync(TEntity entity, CancellationToken cancellation = default);

        /// <summary>
        ///     Returns the T by its given id.
        /// </summary>
        /// <param name="id">The value representing the ObjectId of the entity to retrieve.</param>
        /// <param name="cancellation">The cancellation token.</param>
        /// <returns>The Entity T.</returns>
        Task<TEntity> GetByIdAsync(TEntityKey id, CancellationToken cancellation = default);

        /// <summary>
        ///     Returns a paged list for the specified filters
        /// </summary>
        /// <param name="request">The pagination information.</param>
        /// <param name="filters">The query filters.</param>
        /// <param name="sort">The query sort rules.</param>
        /// <param name="cancellation">The cancellation token.</param>
        /// <returns>The Entity T.</returns>
        Task<IPagedResult<TEntity>> GetPagedListAsync(PagedRequestDTO request, FilterDefinition<TEntity> filters = null, SortDefinition<TEntity> sort = null, CancellationToken cancellation = default);

        /// <summary>
        ///     Adds the new entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <param name="cancellation">The cancellation token.</param>
        /// <returns>The added entity including its new ObjectId.</returns>
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellation = default);

        /// <summary>
        ///     Upserts an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellation">The cancellation token.</param>
        /// <returns>The updated entity.</returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellation = default);

        /// <summary>
        ///     Update the specified documents asynchronous.
        /// </summary>
        /// <param name="documents">The documents to update.</param>
        /// <param name="cancellation">The cancellation token.</param>
        Task<BulkWriteResult<TEntity>> ReplaceManyAsync(IEnumerable<TEntity> documents, CancellationToken cancellation = default);

        /// <summary>
        ///     Deletes an entity from the repository by its id.
        /// </summary>
        /// <param name="id">The entity's id.</param>
        /// <param name="cancellation">The cancellation token.</param>
        Task<DeleteResult> DeleteAsync(TEntityKey id, CancellationToken cancellation = default);

        /// <summary>
        ///     Deletes the given entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <param name="cancellation">The cancellation token.</param>
        Task<DeleteResult> DeleteAsync(TEntity entity, CancellationToken cancellation = default);

        /// <summary>
        ///     Deletes the document found with the specified condition asynchronous.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <param name="cancellation">The cancellation token.</param>
        Task<DeleteResult> DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellation = default);

        /// <summary>
        ///     Deletes all entities in the repository.
        /// </summary>
        //Task DeleteAllAsync(CancellationToken cancellation = default);

        /// <summary>
        ///     Counts the total entities in the repository.
        /// </summary>
        /// <returns>Count of entities in the repository.</returns>
        Task<long> CountAsync(CancellationToken cancellation = default);
    }
}

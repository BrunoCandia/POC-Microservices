using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using ServiceStack;
using Users.API.DTO.Common.Paging.Request;
using Users.API.DTO.Paging.Response;
using Users.API.Infrastructure.Documents;
using Users.API.Infrastructure.Extensions;
using Users.API.Infrastructure.Mongo;

namespace Users.API.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity, TEntityKey> : IRepository<TEntity, TEntityKey> where TEntity : BaseDocument<TEntityKey>//, class
    {
        protected readonly IMongoContext Context;
        protected IMongoCollection<TEntity> DbSet;

        protected BaseRepository(IMongoContext context)
        {
            Context = context;

            // added for the new IRepository
            ConfigDbSet();
        }

        private void ConfigDbSet()
        {
            DbSet = Context.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public virtual async Task AddAsync(TEntity obj)
        {
            ConfigDbSet();

            await DbSet.InsertOneAsync(obj);
        }        

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            ConfigDbSet();

            var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);

            return all.ToList();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            ConfigDbSet();

            var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq(" _id ", id));

            return data.FirstOrDefault();
        }

        public virtual async Task RemoveAsync(Guid id)
        {
            ConfigDbSet();

            await DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq(" _id ", id));
        }

        public virtual async Task UpdateAsync(TEntity obj)
        {
            ConfigDbSet();

            await DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq(" _id ", obj.GetId()), obj);
        }        

        public void Dispose()
        {
            Context?.Dispose();
        }

        public virtual async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellation = default)
        {
            var result = await DbSet.Find(predicate)
                .Limit(1)
                .Project(x => x.Id)
                .FirstOrDefaultAsync(cancellation)
                .ConfigureAwait(false);

            return result != null;
        }

        public virtual Task<bool> ExistAsync(TEntityKey id, CancellationToken cancellation = default)
        {
            return ExistAsync(Builders<TEntity>.Filter.Eq(x => x.Id, id), cancellation);
        }

        public virtual async Task<bool> ExistAsync(FilterDefinition<TEntity> filters, CancellationToken cancellation = default)
        {
            if (filters == null)
            {
                filters = Builders<TEntity>.Filter.Empty;
            }

            var result = await DbSet.Find(filters)
                    .Limit(1)
                    .Project(x => x.Id)
                    .FirstOrDefaultAsync(cancellation)
                    .ConfigureAwait(false);

            return result != null;
        }

        public virtual Task InsertManyAsync(IEnumerable<TEntity> documents, CancellationToken cancellation = default)
        {
            return DbSet.InsertManyAsync(documents, cancellationToken: cancellation);
        }

        public virtual Task<ReplaceOneResult> SaveOrUpdateAsync(Expression<Func<TEntity, bool>> predicate, TEntity entity, CancellationToken cancellation = default)
        {
            return DbSet.ReplaceOneAsync(predicate, entity, new UpdateOptions { IsUpsert = true }, cancellation);
        }

        public virtual Task<ReplaceOneResult> SaveOrUpdateAsync(TEntity entity, CancellationToken cancellation = default)
        {
            var f = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);
            return DbSet.ReplaceOneAsync(f, entity, new UpdateOptions { IsUpsert = true }, cancellation);
        }

        public virtual Task<TEntity> GetByIdAsync(TEntityKey id, CancellationToken cancellation = default)
        {
            var f1 = Builders<TEntity>.Filter.Eq(x => x.Id, id);

            return DbSet.Find(f1)
                .Limit(1)
                .SingleOrDefaultAsync(cancellation);
        }

        public virtual Task<IPagedResult<TEntity>> GetPagedListAsync(PagedRequestDTO request, FilterDefinition<TEntity> filters = null, SortDefinition<TEntity> sort = null, CancellationToken cancellation = default)
        {
            if (filters == null)
            {
                filters = Builders<TEntity>.Filter.Empty;
            }

            return DbSet.ToPagedResultAsync<TEntity, TEntityKey>(filters, request, sort, cancellation);
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellation = default)
        {
            await DbSet.InsertOneAsync(entity, cancellationToken: cancellation).ConfigureAwait(false);

            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellation = default)
        {
            var f = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);
            await DbSet.ReplaceOneAsync(f, entity, cancellationToken: cancellation).ConfigureAwait(false);

            return entity;
        }

        public virtual Task<BulkWriteResult<TEntity>> ReplaceManyAsync(IEnumerable<TEntity> documents, CancellationToken cancellation = default)
        {
            var bulkOps = new List<WriteModel<TEntity>>();

            foreach (var document in documents)
            {
                var filter = Builders<TEntity>.Filter.Eq(x => x.Id, document.Id);
                var updateOne = new ReplaceOneModel<TEntity>(filter, document);

                bulkOps.Add(updateOne);
            }

            return DbSet.BulkWriteAsync(bulkOps, cancellationToken: cancellation);
        }

        public virtual Task<DeleteResult> DeleteAsync(TEntityKey id, CancellationToken cancellation = default)
        {
            var f = Builders<TEntity>.Filter.Eq(x => x.Id, id);
            return DbSet.DeleteOneAsync(f, cancellation);
        }

        public virtual Task<DeleteResult> DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellation = default)
        {
            return DbSet.DeleteOneAsync(predicate, cancellation);
        }

        public virtual Task<DeleteResult> DeleteAsync(TEntity entity, CancellationToken cancellation = default)
        {
            return DeleteAsync(entity.Id, cancellation);
        }

        //public virtual Task DeleteAllAsync(CancellationToken cancellation = default)
        //{
        //    return Database.DropCollectionAsync(CollectionName, cancellation);
        //}

        public virtual Task<long> CountAsync(CancellationToken cancellation = default)
        {
            return DbSet.Find(x => x.Id != null)
                .Limit(1)
                .CountDocumentsAsync(cancellation);
        }
    }
}

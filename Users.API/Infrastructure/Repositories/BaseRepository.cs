using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Users.API.Infrastructure.Mongo;

namespace Users.API.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly IMongoContext Context;
        protected IMongoCollection<TEntity> DbSet;

        protected BaseRepository(IMongoContext context)
        {
            Context = context;
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
    }
}

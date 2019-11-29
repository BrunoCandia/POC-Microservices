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

        public virtual void Add(TEntity obj)
        {
            throw new NotImplementedException();
        }        

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            ConfigDbSet();

            var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);

            return all.ToList();
        }

        public virtual Task<TEntity> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(TEntity obj)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}

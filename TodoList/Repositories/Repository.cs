using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TodoList.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext dbContext;

        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<TEntity> Get(int id)
        {
            return dbContext.Set<TEntity>().FindAsync(id);
        }

        public Task<List<TEntity>> GetAll()
        {
            return dbContext.Set<TEntity>().ToListAsync();
        }

        public Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return dbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public void Add(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            dbContext.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            dbContext.Set<TEntity>().RemoveRange(entities);
        }
    }
}

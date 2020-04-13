using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MakeATrinkspruch.Api.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDBContext dbContext;

        public Repository(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Count()
        {
            return dbContext.Set<T>().Count();
        }

        public virtual async Task<int> CountAsync()
        {
            return await dbContext.Set<T>().CountAsync();
        }

        public virtual T Create(T entity)
        {
            dbContext.Set<T>().Add(entity);
            dbContext.SaveChanges();
            return entity;
        }

        public virtual int Delete(Expression<Func<T, bool>> identifierexpression)
        {
            T entity = Get(identifierexpression);
            dbContext.Set<T>().Remove(entity);
            return dbContext.SaveChanges();
        }

        public virtual async Task<int> DeleteAsync(Expression<Func<T, bool>> identifierexpression)
        {
            T entity = await GetAsync(identifierexpression);
            dbContext.Set<T>().Remove(entity);
            return await dbContext.SaveChangesAsync();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbContext.Set<T>().ToList();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public virtual T Get(Expression<Func<T, bool>> identifierexpression)
        {
            return dbContext.Set<T>().FirstOrDefault(identifierexpression);
        }

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> identifierexpression)
        {
            return await dbContext.Set<T>().FirstOrDefaultAsync(identifierexpression);
        }

        public virtual T Update(T updated)
        {
            if (updated == null)
            {
                throw new ArgumentException("Angegebene Entity ist null");
            }
            dbContext.Update(updated);
            dbContext.SaveChanges();
            return updated;
        }

        public virtual async Task<T> UpdateAsync(T updated)
        {
            if (updated == null)
            {
                throw new ArgumentException("Angegebene Entity ist null");
            }

            dbContext.Update(updated);
            await dbContext.SaveChangesAsync();
            return updated;
        }
    }
}
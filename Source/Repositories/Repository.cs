using MakeATrinkspruch.Api.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MakeATrinkspruch.Api.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDBContext dbContext;

        public Repository(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Count()
        {
            return dbContext.Set<T>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await dbContext.Set<T>().CountAsync();
        }

        public void Create(T entity)
        {
            dbContext.Set<T>().Add(entity);
        }

        public int Delete(Expression<bool> identifierexpression)
        {
            T entity = GetById(identifierexpression);
            dbContext.Set<T>().Remove(entity);
            return dbContext.SaveChanges();
        }

        public async Task<int> DeleteAsync(Expression<bool> identifierexpression)
        {
            T entity = await GetByIdAsync(identifierexpression);
            dbContext.Set<T>().Remove(entity);
            return await dbContext.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll()
        {
            return dbContext.Set<T>().ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public T GetById(Expression<bool> identifierexpression)
        {
            return dbContext.Set<T>().Find(identifierexpression);
        }

        public async Task<T> GetByIdAsync(Expression<bool> identifierexpression)
        {
            return await dbContext.Set<T>().FindAsync(identifierexpression);
        }

        public T Update(T updated, Expression<bool> identifierExpression)
        {
            if (updated == null)
                return null;

            T existing = GetById(identifierExpression);

            if (existing != null)
            {
                dbContext.Entry(existing).CurrentValues.SetValues(updated);
                dbContext.SaveChanges();
            }
            return existing;
        }

        public async Task<T> UpdateAsync(T updated, Expression<bool> identifierExpression)
        {
            if (updated == null)
                return null;

            T existing = await GetByIdAsync(identifierExpression);

            if (existing != null)
            {
                dbContext.Entry(existing).CurrentValues.SetValues(updated);
                await dbContext.SaveChangesAsync();
            }
            return existing;
        }
    }
}
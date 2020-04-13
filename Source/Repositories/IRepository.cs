using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MakeATrinkspruch.Api.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Get(Expression<Func<T, bool>> identifierexpression);

        Task<T> GetAsync(Expression<Func<T, bool>> identifierexpression);

        IEnumerable<T> GetAll();

        Task<IEnumerable<T>> GetAllAsync();

        T Update(T updated);

        Task<T> UpdateAsync(T updated);

        T Create(T entity);

        int Delete(Expression<Func<T, bool>> identifierexpression);

        Task<int> DeleteAsync(Expression<Func<T, bool>> identifierexpression);

        public int Count();

        Task<int> CountAsync();
    }
}
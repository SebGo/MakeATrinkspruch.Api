using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MakeATrinkspruch.Api.Repositories
{
    public interface IRepository<T> where T : class
    {
        T GetById(Expression<bool> identifierexpression);

        Task<T> GetByIdAsync(Expression<bool> identifierexpression);

        IEnumerable<T> GetAll();

        Task<IEnumerable<T>> GetAllAsync();

        T Update(T entity, Expression<bool> identifierexpression);

        Task<T> UpdateAsync(T updated, Expression<bool> identifierexpression);

        void Create(T entity);

        int Delete(Expression<bool> identifierexpression);

        Task<int> DeleteAsync(Expression<bool> identifierexpression);

        public int Count();

        Task<int> CountAsync();
    }
}
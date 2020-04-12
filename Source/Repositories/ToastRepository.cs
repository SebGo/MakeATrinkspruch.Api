using MakeATrinkspruch.Api.Data.Entities;
using MakeATrinkspruch.Api.Database;

namespace MakeATrinkspruch.Api.Repositories
{
    public class ToastRepository : Repository<Toast>
    {
        public ToastRepository(AppDBContext dbContext) : base(dbContext)
        {
        }
    }
}
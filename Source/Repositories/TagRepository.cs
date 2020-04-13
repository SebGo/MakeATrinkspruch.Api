using MakeATrinkspruch.Api.Data.Entities;

namespace MakeATrinkspruch.Api.Repositories
{
    public class TagRepository : Repository<Tag>
    {
        public TagRepository(AppDBContext dbContext) : base(dbContext)
        {
        }
    }
}
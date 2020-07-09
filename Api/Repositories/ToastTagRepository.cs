using MakeATrinkspruch.Data;
using MakeATrinkspruch.Data.Entities;
using System;
using System.Collections.Generic;

namespace MakeATrinkspruch.Api.Repositories
{
    public class ToastTagRepository : Repository<ToastTag>
    {
        public ToastTagRepository(AppDBContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<ToastTag> CreateRange(IEnumerable<ToastTag> toastTags)
        {
            dbContext.AddRange(toastTags);
            dbContext.SaveChanges();
            return toastTags;
        }
    }
}
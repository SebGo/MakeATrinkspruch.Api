﻿using MakeATrinkspruch.Api.Data.Entities;
using MakeATrinkspruch.Api.Data.TransferObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MakeATrinkspruch.Api.Repositories
{
    public class ToastRepository : Repository<Toast>
    {
        public ToastRepository(AppDBContext dbContext) : base(dbContext)
        {
        }

        public override IEnumerable<Toast> GetAll()
        {
            List<Toast> toasts = dbContext.Toasts.Include(t => t.ToastTags).ThenInclude(tt => tt.Tag).ToList();
            return toasts;
        }

        public override async Task<IEnumerable<Toast>> GetAllAsync()
        {
            List<Toast> toasts = await dbContext.Toasts.Include(t => t.ToastTags).ThenInclude(tt => tt.Tag).ToListAsync();
            return toasts;
        }

        public override Toast Get(Expression<Func<Toast, bool>> identifierexpression)
        {
            return dbContext.Set<Toast>().Include(t => t.ToastTags).ThenInclude(tt => tt.Tag).FirstOrDefault(identifierexpression);
        }

        public override async Task<Toast> GetAsync(Expression<Func<Toast, bool>> identifierexpression)
        {
            return await dbContext.Set<Toast>().Include(t => t.ToastTags).ThenInclude(tt => tt.Tag).FirstOrDefaultAsync(identifierexpression);
        }

        public async Task<IEnumerable<Toast>> GetFilteredByTagIds(List<Guid> tagIds)
        {
            return await dbContext.ToastTag
                .Where(x => tagIds.Contains(x.TagId)) // filtering goes here
                .Select(x => x.Toast)
                .Distinct()
                .ToListAsync();
        }
    }
}
using MakeATrinkspruch.Api.Data.Entities;
using MakeATrinkspruch.Api.Database;
using MakeATrinkspruch.Api.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeATrinkspruch.Api.DataServices
{
    public class ToastDataService
    {
        private readonly ToastRepository repository;
        private readonly Random random;
        private readonly ILogger<ToastDataService> logger;

        public ToastDataService(AppDBContext dbContext, ILoggerFactory loggerFactory)
        {
            repository = new ToastRepository(dbContext);
            random = new Random();
            this.logger = loggerFactory.CreateLogger<ToastDataService>();
        }

        public async Task<string> GetRandomToast()
        {
            IEnumerable<Toast> toast = await repository.GetAllAsync();
            int numberOfTrinksruechen = await repository.CountAsync();
            int index = random.Next(0, numberOfTrinksruechen - 1);
            string res = toast.ElementAt(index).ToastText;
            return res;
        }
    }
}
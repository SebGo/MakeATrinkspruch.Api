using MakeATrinkspruch.Api.Data.Entities;
using MakeATrinkspruch.Api.Database;
using MakeATrinkspruch.Api.DataServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MakeATrinkspruch.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToastController : ControllerBase
    {
        private readonly ILogger<ToastController> logger;
        private readonly ToastDataService dataService;

        public ToastController(AppDBContext dbContext, ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<ToastController>();
            this.dataService = new ToastDataService(dbContext, loggerFactory);
        }

        /*
        [HttpPut]
        public ActionResult<Toast> InsertNewToast(Toast toast)
        {
            try
            {
                if (toast == null)
                {
                    return
                }
                var res = dataService.Create(toast);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }*/

        [HttpGet]
        public async Task<ActionResult<string>> GetRandomToast()
        {
            try
            {
                string res = await dataService.GetRandomToast();
                return Ok(res);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
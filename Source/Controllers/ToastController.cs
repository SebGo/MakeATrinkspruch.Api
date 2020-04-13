using MakeATrinkspruch.Api.Data.TransferObjects;
using MakeATrinkspruch.Api.DataServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

        [HttpPost]
        public ActionResult<ToastDto> CreateNewToast([FromBody]ToastDto toast)
        {
            try
            {
                if (toast == null)
                {
                    return BadRequest();
                }

                ToastDto res = dataService.Create(toast);
                return Ok(res);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ToastDto>> UpdateToast(Guid id, [FromBody]ToastDto toastDto)
        {
            try
            {
                if (toastDto == null || (toastDto.Id != id))
                {
                    return BadRequest();
                }

                ToastDto res = await dataService.UpdateAsync(id, toastDto);
                return Ok(res);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetRandom")]
        public async Task<ActionResult<ToastDto>> GetRandomToast()
        {
            try
            {
                ToastDto res = await dataService.GetRandomToast();
                return Ok(res);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ToastDto>>> GetAll()
        {
            try
            {
                IEnumerable<ToastDto> res = await dataService.GetAllToastsAsync();
                return Ok(res);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteToastAsync(Guid id)
        {
            try
            {
                await dataService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
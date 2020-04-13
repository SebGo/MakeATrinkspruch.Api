using MakeATrinkspruch.Api.Data.TransferObjects;
using MakeATrinkspruch.Api.DataServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MakeATrinkspruch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ILogger<TagController> logger;

        private readonly TagDataService dataService;

        public TagController(AppDBContext dbContext, ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<TagController>();
            this.dataService = new TagDataService(dbContext, loggerFactory);
        }

        [HttpPost]
        public ActionResult<TagDto> CreateNewTag([FromBody]TagDto tag)
        {
            try
            {
                if (tag == null)
                {
                    return BadRequest();
                }

                TagDto res = dataService.Create(tag);
                return Ok(res);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<TagDto>> GetAllTags()
        {
            try
            {
                IEnumerable<TagDto> res = await dataService.GetAllAsync();
                return Ok(res);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<ToastDto>> UpdateAsync(Guid id, TagDto tagDto)
        {
            try
            {
                if (tagDto == null || (tagDto.Id != id))
                {
                    return BadRequest();
                }

                TagDto res = await dataService.UpdateAsync(id, tagDto);
                return Ok(res);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTagAsync(Guid id)
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
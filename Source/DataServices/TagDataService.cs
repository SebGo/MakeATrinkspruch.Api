using MakeATrinkspruch.Api.Data.Entities;
using MakeATrinkspruch.Api.Data.TransferObjects;
using MakeATrinkspruch.Api.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MakeATrinkspruch.Api.DataServices
{
    public class TagDataService
    {
        private readonly TagRepository tagRepository;
        private readonly ILogger<TagDataService> logger;

        public TagDataService(AppDBContext dbContext, ILoggerFactory loggerFactory)
        {
            tagRepository = new TagRepository(dbContext);
            logger = loggerFactory.CreateLogger<TagDataService>();
        }

        public TagDto Create(TagDto tagDto)
        {
            Tag existingTag = tagRepository.Get(t => t.TagName == tagDto.TagName);

            if (existingTag != null)
            {
                throw new Exception("Tag existiert bereits");
            }

            Tag tag = new Tag()
            {
                TagName = tagDto.TagName,
            };

            tag = tagRepository.Create(tag);
            return tag.ToDto();
        }

        public async Task<IEnumerable<TagDto>> GetAllAsync()
        {
            IEnumerable<Tag> tags = await tagRepository.GetAllAsync();
            List<TagDto> tagDtos = new List<TagDto>();
            foreach (Tag tag in tags)
            {
                tagDtos.Add(tag.ToDto());
            }
            return tagDtos;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            return await this.tagRepository.DeleteAsync(t => t.Id == id);
        }

        internal async Task<TagDto> UpdateAsync(Guid id, TagDto updatedTag)
        {
            if (updatedTag == null || id != updatedTag.Id)
            {
                throw new ArgumentException("Angegebener Tag ist null oder Id stimmt nicht mit der Id des Tags überein.");
            }

            Tag existingTag = await tagRepository.GetAsync(t => t.Id == id);

            if (existingTag == null)
            {
                throw new ArgumentException("Zu der angegebenen Id existiert kein Tag");
            }

            existingTag.TagName = updatedTag.TagName;

            Tag res = await tagRepository.UpdateAsync(existingTag);
            return res.ToDto();
        }
    }
}
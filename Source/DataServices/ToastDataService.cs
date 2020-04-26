using MakeATrinkspruch.Api.Data.Entities;
using MakeATrinkspruch.Api.Data.TransferObjects;
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
        private readonly ToastRepository toastRepository;
        private readonly TagRepository tagRepository;
        private readonly ToastTagRepository toastTagRepository;
        private readonly Random random;
        private readonly ILogger<ToastDataService> logger;

        public ToastDataService(AppDBContext dbContext, ILoggerFactory loggerFactory)
        {
            toastRepository = new ToastRepository(dbContext);
            tagRepository = new TagRepository(dbContext);
            toastTagRepository = new ToastTagRepository(dbContext);
            random = new Random();
            this.logger = loggerFactory.CreateLogger<ToastDataService>();
        }

        public ToastDto Create(ToastDto entity)
        {
            Toast toast = new Toast()
            {
                Id = Guid.Empty,
                ToastText = entity.ToastText,
                Reviewed = false,
            };
            Toast res = toastRepository.Create(toast);

            try
            {
                List<ToastTag> toastTags = new List<ToastTag>();
                foreach (TagDto tagDto in entity.Tags)
                {
                    Tag tag = tagRepository.Get(t => t.Id == tagDto.Id);
                    if (tag == null)
                    {
                        throw new ArgumentException("Ein Tag des Trinkspruches konnte nicht gefunden werden");
                    }

                    ToastTag toastTag = new ToastTag()
                    {
                        TagId = tag.Id,
                        ToastId = toast.Id
                    };
                    toastTags.Add(toastTag);
                }
                toastTagRepository.CreateRange(toastTags);
                return res.ToDto();
            }
            catch (Exception e)
            {
                toastRepository.Delete(t => t.Id == toast.Id);
                logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<ToastDto> GetRandomToast(List<Guid> tagIds)
        {
            IEnumerable<Toast> toasts;
            if (!tagIds.Any())
            {
                toasts = await toastRepository.GetAllAsync();
            }
            else
            {
                toasts = await toastRepository.GetFilteredByTagIds(tagIds);
            }
            int numberOfToasts = toasts.Count();
            if (numberOfToasts == 0)
            {
                return new ToastDto();
            }

            int index = random.Next(0, numberOfToasts);
            Toast res = toasts.ElementAt(index);
            return res.ToDto();
        }

        internal async Task<IEnumerable<ToastDto>> GetAllToastsAsync()
        {
            IEnumerable<Toast> toasts = await toastRepository.GetAllAsync();
            List<ToastDto> toastDtos = new List<ToastDto>();
            foreach (Toast toast in toasts)
            {
                toastDtos.Add(toast.ToDto());
            }
            return toastDtos;
        }

        internal async Task<int> DeleteAsync(Guid id)
        {
            return await toastRepository.DeleteAsync(t => t.Id == id);
        }

        public async Task<ToastDto> UpdateAsync(Guid id, ToastDto updatedToast)
        {
            if (updatedToast == null || id != updatedToast.Id)
            {
                throw new ArgumentException("Angegebener Trinkspruch ist null oder Id stimmt nicht mit der Id des Trinkspruches überein.");
            }

            Toast existingToast = await toastRepository.GetAsync(t => t.Id == id);

            if (existingToast == null)
            {
                throw new ArgumentException("Angegebene Id ist falsch");
            }

            existingToast.Reviewed = updatedToast.Reviewed;
            existingToast.ToastText = updatedToast.ToastText;

            try
            {
                List<Tag> existingTags = existingToast.ToastTags.Select(tt => tt.Tag).ToList();
                List<Tag> updatedTags = new List<Tag>();
                foreach (TagDto tagDto in updatedToast.Tags)
                {
                    Tag tag = await tagRepository.GetAsync(t => t.Id == tagDto.Id);
                    updatedTags.Add(tag);
                }

                List<Tag> tagsToAdd = updatedTags.Except(existingTags).ToList();
                foreach (Tag tagToAdd in tagsToAdd)
                {
                    ToastTag toastTagToAdd = new ToastTag()
                    {
                        TagId = tagToAdd.Id,
                        ToastId = existingToast.Id
                    };
                    toastTagRepository.Create(toastTagToAdd);
                }

                List<Tag> tagsToDelete = existingTags.Except(updatedTags).ToList();
                foreach (Tag tagToDelete in tagsToDelete)
                {
                    toastTagRepository.Delete(tt => tt.TagId == tagToDelete.Id && tt.ToastId == existingToast.Id);
                }

                Toast res = await toastRepository.UpdateAsync(existingToast);
                return res.ToDto();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                throw;
            }
        }
    }
}
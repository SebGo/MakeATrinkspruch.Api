using MakeATrinkspruch.Data.TransferObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MakeATrinkspruch.Data.Entities
{
    public class Toast
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string ToastText { get; set; }

        [Required]
        public bool Reviewed { get; set; }

        public IEnumerable<ToastTag> ToastTags { get; set; }

        public ToastDto ToDto()
        {
            List<TagDto> tags = new List<TagDto>();

            if (ToastTags != null)
            {
                foreach (ToastTag toastTag in ToastTags)
                {
                    tags.Add(toastTag.Tag.ToDto());
                }

                ToastDto dto = new ToastDto()
                {
                    Id = this.Id,
                    ToastText = this.ToastText,
                    Reviewed = this.Reviewed,
                    Tags = tags
                };
                return dto;
            }
            else
            {
                ToastDto dto = new ToastDto()
                {
                    Id = this.Id,
                    ToastText = this.ToastText,
                    Reviewed = this.Reviewed,
                    Tags = new List<TagDto>()
                };
                return dto;
            }
        }
    }
}
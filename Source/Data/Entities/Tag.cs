using MakeATrinkspruch.Api.Data.TransferObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MakeATrinkspruch.Api.Data.Entities
{
    public class Tag
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string TagName { get; set; }

        public IEnumerable<ToastTag> ToastTags { get; set; }

        internal TagDto ToDto()
        {
            return new TagDto()
            {
                Id = this.Id,
                TagName = this.TagName
            };
        }
    }
}
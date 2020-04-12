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

        public Tag Clone()
        {
            string serialized = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Tag>(serialized);
        }
    }
}
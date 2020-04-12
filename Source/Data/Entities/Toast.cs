using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MakeATrinkspruch.Api.Data.Entities
{
    public class Toast
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string ToastText { get; set; }

        public IEnumerable<ToastTag> ToastTags { get; set; }

        public Toast Clone()
        {
            string serialized = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Toast>(serialized);
        }
    }
}
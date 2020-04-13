using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeATrinkspruch.Api.Data.TransferObjects
{
    public class ToastDto
    {
        public Guid Id { get; set; }
        public string ToastText { get; set; }
        public bool Reviewed { get; set; }
        public IEnumerable<TagDto> Tags { get; set; }
    }
}
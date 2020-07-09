using System;
using System.Collections.Generic;

namespace MakeATrinkspruch.Data.TransferObjects
{
    public class ToastDto
    {
        public Guid Id { get; set; }
        public string ToastText { get; set; }
        public bool Reviewed { get; set; }
        public IEnumerable<TagDto> Tags { get; set; }
    }
}
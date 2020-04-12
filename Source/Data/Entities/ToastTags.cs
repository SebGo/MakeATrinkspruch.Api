using System;

namespace MakeATrinkspruch.Api.Data.Entities
{
    public class ToastTag
    {
        public Guid ToastId { get; set; }
        public Toast Toast { get; set; }

        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
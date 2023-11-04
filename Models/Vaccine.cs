using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;

namespace ASP_Final.Models
{
    public class Vaccine
    {
        public int Id { get; set; }

        public string Name { get; set;}

        public string Country { get; set; }

        public DateTime ExpirationData { get; set; }

        public Double Price { get; set; }

        [DisplayName("Type")]
        public int TypeId { get; set; }
        [ValidateNever]
        public Type Type { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;
    }

}


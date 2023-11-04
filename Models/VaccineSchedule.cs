using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP_Final.Models
{
    public class VaccineSchedule
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        [DisplayName("Vaccination Dates")]
        public string VaccinationDates { get; set; } = "";
        [Required]
        [DisplayName("Vaccine")]
        public int VaccineId { get; set; }
        [ValidateNever]
        public Vaccine Vaccine { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}

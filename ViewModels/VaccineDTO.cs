using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ASP_Final.Models;

namespace ASP_MVC.ViewModels
{
    public class VaccineDTO
    {
        public Vaccine Vaccine { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> TypeList { get; set; }

    }
}

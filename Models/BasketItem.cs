using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ASP_Final.Models
{
    public class BasketItem
    {
        public Vaccine Vaccine { get; set; }

        public int Count { get; set; }
    }
}

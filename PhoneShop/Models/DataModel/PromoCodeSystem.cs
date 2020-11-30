using System;
using System.ComponentModel.DataAnnotations;
namespace PhoneShop.Models.DataModel
{
    public class PromoCodeSystem
    {
        public int Id;
        [Required(ErrorMessage = "Enter First Date")]
        public DateTime Date1;
        [Required(ErrorMessage = "Enter Second Date")]
        public DateTime Date2;
        [Required(ErrorMessage = "Enter DiscountPercentage")]
        [Range(1, 100, ErrorMessage = "Out of range")]
        public int DiscountPercentage;
        [Required(ErrorMessage = "Enter text of promocode")]
        public string PromoCode;
    }
}

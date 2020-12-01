using System;
using System.ComponentModel.DataAnnotations;
namespace PhoneShop.Models.DataModel
{
    public class PromoCodeSystem
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter First Date")]
        public DateTime Date1 { get; set; }
        [Required(ErrorMessage = "Enter Second Date")]
        public DateTime Date2 { get; set; }
        [Required(ErrorMessage = "Enter DiscountPercentage")]
        [Range(1, 100, ErrorMessage = "Out of range")]
        public int DiscountPercentage { get; set; }
        [Required(ErrorMessage = "Enter text of promocode")]
        public string PromoCode { get; set; }
    }
}

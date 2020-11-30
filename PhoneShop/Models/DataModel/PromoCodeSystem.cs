using System;
using System.ComponentModel.DataAnnotations;
namespace PhoneShop.Models.DataModel
{
    public class PromoCodeSystem
    {
        public int Id;
        public DateTime Date1;
        public DateTime Date2;
        public int DiscountPercentage;
        public string PromoCode;
    }
}

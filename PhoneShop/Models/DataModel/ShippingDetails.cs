using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneShop.Models.DataModel
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Enter your name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter your surname")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Enter your email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Incorrect email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter your Country")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Enter your City")]
        public string City { get; set; }
        [Required(ErrorMessage = "Enter your Address")]
        public string Address { get; set; }
    }
}

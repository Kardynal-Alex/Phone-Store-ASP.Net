using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter your name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Choose brand")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "Enter Price")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Out of range")]
        public decimal Price { get; set; }
        public byte[] Image { get; set; }
    }
}

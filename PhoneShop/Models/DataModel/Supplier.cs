using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneShop.Models.DataModel
{
    public class Supplier
    {
        public int Id { get; set; }
        [Display(Name = "Name of Corporation")]
        [Required(ErrorMessage = "Enter Name Corporation")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Country Supplier")]
        public string Country { get; set; }
        public int ContactDetailId { get; set; }
        public ContactDetail ContactDetail { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}

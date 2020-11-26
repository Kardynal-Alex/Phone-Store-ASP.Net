using System.ComponentModel.DataAnnotations;

namespace PhoneShop.Models.DataModel
{
    public class ContactDetail
    {
        public int Id { get; set; }
        [Display(Name = "Supplier Name")]
        [Required(ErrorMessage = "Enter Name Supplier")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Phone Supplier")]
        public int Phone { get; set; }
    }
}

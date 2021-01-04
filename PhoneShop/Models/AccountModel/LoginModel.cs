using System.ComponentModel.DataAnnotations;
namespace PhoneShop.Models.AccountModel
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

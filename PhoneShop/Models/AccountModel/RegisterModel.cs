using System.ComponentModel.DataAnnotations;
namespace PhoneShop.Models.AccountModel
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Enter Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}

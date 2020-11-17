﻿using System.ComponentModel.DataAnnotations;

namespace PhoneShop.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
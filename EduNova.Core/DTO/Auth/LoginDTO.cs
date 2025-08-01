﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Core.DTO.Auth
{
    public class LoginDTO
    {
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";
    }
}

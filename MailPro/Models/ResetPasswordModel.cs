using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MailPro.Models
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Please Enter your new password")]
        [MinLength(7, ErrorMessage = "Minimum 7 charracters required")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please confirm your password")]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
        public string ResetCode { get; set; }
    }
}
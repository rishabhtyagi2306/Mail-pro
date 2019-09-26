using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MailPro.Models
{
    public class Membership
    {
        public int FacultyID { get; set; }

        [Required]
        public string FacultyName { get; set; }

        [EmailAddress]
        public string FacultyEmail { get; set; }

        [Required]
        [MinLength(10,  ErrorMessage = "Please enter a valid Phone Number")]
        public string FacultyPhoneNo { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        [MinLength(7,ErrorMessage = "Minimum 7 charracters required")]
        //[RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{7,}$", ErrorMessage = "Password must be atleast 7 characters long with Atleast one capital letter,Number and Special symbol (e.g. !@#$%^&*)")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm Your Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string ActivationCode { get; set; }
        public bool IsEmailVerified { get; set; }
        //public virtual ICollection<CategoryTable> CategoryTable { get; set; }
    }
}
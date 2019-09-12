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
        public int FacultyPhoneNo { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please conform Your Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        //public virtual ICollection<CategoryTable> CategoryTable { get; set; }
    }
}
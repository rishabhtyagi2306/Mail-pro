using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MailPro.Models
{
    public class Email
    {
        private FacultyTable facultyTable;

        public int MailID { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        [AllowHtml]
        public string Contents { get; set; }
        [EmailAddress]
        public string Sent { get; set; }

        [ForeignKey("FacultyID")]
        public int FacultyID { get; set; }
        // public virtual Membership FacultyTable { get => facultyTable; set => facultyTable = value; }

        public string GmailPassword { get; set; }
        public virtual FacultyTable FacultyTable { get => facultyTable; set => facultyTable = value; }
    }
}
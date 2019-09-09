using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailPro.Models
{
    public class CCategory
    {
        private Membership facultyTable;

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        [Display(Name = "FacultyID")]
        public int FacultyID { get; set; }
        //public int FacultyID { get; set; }
        [ForeignKey("FacultyID")]
       // public virtual Membership FacultyTable { get; set; }
       
        public virtual Membership FacultyTable { get => facultyTable; set => facultyTable = value; }

    }
}
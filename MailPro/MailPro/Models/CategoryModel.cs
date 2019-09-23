using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MailPro.Models
{
    public class CategoryModel
    {
        private Membership facultyTable;

        public int CategoryID { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [ForeignKey("FacultyID")]
        public int FacultyID { get; set; }
        //public int FacultyID { get; set; }
        
        // public virtual Membership FacultyTable { get; set; }

        public virtual Membership FacultyTable { get => facultyTable; set => facultyTable = value; }
        public virtual ICollection<CategoryTable> categoryTables { get; set; }


    }
}
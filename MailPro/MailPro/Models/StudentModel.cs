using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MailPro.Models
{
    public class StudentModel
    {
        [Required]
        public int StudentNo { get; set; }

        [Required]
        public string StudentName { get; set; }

        [EmailAddress]
        public string StudentEmail { get; set; }

        [Required]
        public string Branch { get; set; }

        [Required]
        public int Section { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public bool IsHosteller { get; set; }

        [Required]
        public bool IsCR { get; set; }
        public string StudentCategory { get; set; }
        
        public virtual ICollection<CategoryTable> categoryTables { get; set; }
       
    }

    public class ModelOfStudent
    {
        public List<StudentTable> StudentTables { get; set; }
    }
}
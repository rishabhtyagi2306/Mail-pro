using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.WebPages.Html;

namespace MailPro.Models
{
    public class Students
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class StudentModel
    {
        [Required]
        public int StudentNo { get; set; }

        internal IEnumerable<SelectListItem> ToSelectList(Func<object, object> p1, Func<object, object> p2)
        {
            throw new NotImplementedException();
        }

        [Required]
        public string StudentName { get; set; }

        internal IEnumerable<SelectListItem> ToSelectList()
        {
            throw new NotImplementedException();
        }

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
      
        public virtual ICollection<ConnectTable> ConnectTable { get; set; }
        
    }
    
}
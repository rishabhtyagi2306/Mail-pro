using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace demoModel.Model
{
    public class StudentsModel
    {
        public string Name { get; set; }
        [Required]
        public int Section { get; set; }
        public int StudentNo { get; set; }
        
        public string Branch { get; set; }

        [EmailAddress]
        public string EmailId { get; set; }
        
    }
}

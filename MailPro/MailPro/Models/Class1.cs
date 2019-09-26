using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailPro.Models
{
    public class Class1
    {
        public string StudentName { get; set; }
        public string StudentEmail { get; set; }
        public int StuidentNo { get; set; }
        public string Branch { get; set; }
        public int Section { get; set; }
        public int Year { get; set; }
        public bool IsHosteller { get; set; }
        public bool IsCR { get; set; }
        public string StudentCategory { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int FacultyID { get; set; }
        public int Keys { get; set; }
        public bool IsSelected { get; set; }
        public List<Class1> StudentNames { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailPro.Models
{
    public class ViewModel
    {
        public StudentModel studentmodelvm { get; set; }
        public CategoryModel categorymodelvm { get; set; }
        public bool IsSelected { get; set; }
    }
}
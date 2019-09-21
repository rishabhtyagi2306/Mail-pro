using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace MailPro.Models
{
    public class ViewModel
    {
        public StudentModel studentmodelvm { get; set; }
        public CategoryModel categorymodelvm { get; set; }
        public bool IsSelected { get; set; }

     //   public static IEnumerable<ViewModel> Colors = new List<ViewModel>();
       

        }
}
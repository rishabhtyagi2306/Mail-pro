using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
//using System.Web.WebPages.Html;

namespace MailPro.Models
{
    public class DisplayClass
    {
        public int[] ids { get; set; }
        public List<SelectListItem> StudentNames { get; set; }

        public List<SelectListItem> CategoryNames { get; set; }

    }
}
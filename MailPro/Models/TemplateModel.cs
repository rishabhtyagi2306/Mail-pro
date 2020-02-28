using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailPro.Models
{
    public class TemplateModel
    {
        public int TemplateID { get; set; }
        public string TemplateURL { get; set; }
        public string TemplateName { get; set; }
        public string TemplateImage { get; set; }
    }
}
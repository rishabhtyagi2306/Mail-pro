using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MailPro.Models
{
    public class ConnectModel
    {
        public int StudentNo { get; set; }
        public int CategoryID { get; set; }
        public int PrimaryID { get; set; }
        public List<StudentTable> studentTables { get; set; }
        public List<CategoryTable> CategoryTables { get; set; }
    }
}
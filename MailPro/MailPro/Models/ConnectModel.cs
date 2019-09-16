﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MailPro.Models
{
    public class ConnectModel
    {
        [Key]
        [Column(Order = 1)]
        public int StudentNo { get; set; }

        [Key]
        [Column(Order = 2)]
        public int CategoryID { get; set; }
        public int PrimaryID { get; set; }

        public virtual CategoryTable CategoryTable { get; set; }
        public virtual StudentTable StudentTable { get; set; }
    }
}
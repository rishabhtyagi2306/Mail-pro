using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MailPro.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("MailProEntities") { }
        public DbSet<StudentTable> StudentTables { get; set; }

        public DbSet<CategoryTable> CategoryTables { get; set; }

        public DbSet<ConnectTable> ConnectTables { get; set; } 

        
        
    }
}
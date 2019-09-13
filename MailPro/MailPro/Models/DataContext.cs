using System;
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
<<<<<<< HEAD:MailPro/MailPro/Models/DataContext.cs

        public DbSet<CategoryTable> CategoryTables { get; set; }
=======
>>>>>>> e0d1bf4d32031bd60e142c9cdc4c0eeea52c658a:MailPro/Models/DataContext.cs
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MailPro
{
    using System;
    using System.Collections.Generic;
    
    public partial class StudentTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StudentTable()
        {
            this.ConnectTable = new HashSet<ConnectTable>();
        }
    
        public int StudentNo { get; set; }
        public string StudentName { get; set; }
        public string StudentEmail { get; set; }
        public string Branch { get; set; }
        public int Section { get; set; }
        public int Year { get; set; }
        public bool IsHosteller { get; set; }
        public bool IsCR { get; set; }
        public string StudentCategory { get; set; }
        public int FacultyID { get; set; }
        
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConnectTable> ConnectTable { get; set; }
    }
}

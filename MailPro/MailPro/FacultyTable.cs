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
    
    public partial class FacultyTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FacultyTable()
        {
            this.CategoryTable = new HashSet<CategoryTable>();
            this.Mails = new HashSet<Mails>();
        }
    
        public int FacultyID { get; set; }
        public string FacultyName { get; set; }
        public string FacultyEmail { get; set; }
        public int FacultyPhoneNo { get; set; }
        public string Department { get; set; }
        public string Password { get; set; }
        public Nullable<bool> IsEmailVerified { get; set; }
        public Nullable<System.Guid> ActivationCode { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CategoryTable> CategoryTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mails> Mails { get; set; }
    }
}

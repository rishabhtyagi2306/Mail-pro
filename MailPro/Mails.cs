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
    
    public partial class Mails
    {
        public int MailID { get; set; }
        public string Subject { get; set; }
        public string Contents { get; set; }
        public string Sent { get; set; }
        public int FacultyID { get; set; }
        public string GmailPassword { get; set; }
        public string MailPreview { get; set; }
    
        public virtual FacultyTable FacultyTable { get; set; }
    }
}
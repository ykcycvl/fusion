//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fusion.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Mail
    {
        public Mail()
        {
            this.CRMToSend = new HashSet<CRMToSend>();
        }
    
        public int id { get; set; }
        public System.DateTime CreateDateTime { get; set; }
        public string MailSubject { get; set; }
        public int MailTemplateID { get; set; }
    
        public virtual ICollection<CRMToSend> CRMToSend { get; set; }
        public virtual EmailTemplate EmailTemplate { get; set; }
    }
}
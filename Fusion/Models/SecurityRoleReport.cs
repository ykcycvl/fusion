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
    
    public partial class SecurityRoleReport
    {
        public System.Guid Id { get; set; }
        public System.Guid SecurityRoleId { get; set; }
        public System.Guid ReportId { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime Modified { get; set; }
        public long SequenceValue { get; set; }
    
        public virtual Report Report { get; set; }
        public virtual SecurityRole SecurityRole { get; set; }
    }
}

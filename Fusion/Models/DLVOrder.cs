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
    
    public partial class DLVOrder
    {
        public DLVOrder()
        {
            this.DLVOrderLog = new HashSet<DLVOrderLog>();
        }
    
        public int id { get; set; }
        public int SiteOrderID { get; set; }
        public System.DateTime SendDateTime { get; set; }
        public bool Success { get; set; }
    
        public virtual ICollection<DLVOrderLog> DLVOrderLog { get; set; }
    }
}

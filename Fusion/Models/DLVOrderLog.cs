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
    
    public partial class DLVOrderLog
    {
        public int id { get; set; }
        public int OrderID { get; set; }
        public System.DateTime ActionDateTime { get; set; }
        public string ActionName { get; set; }
        public string ActionContent { get; set; }
        public string ActionResult { get; set; }
        public string ActionXML { get; set; }
    
        public virtual DLVOrder DLVOrder { get; set; }
    }
}

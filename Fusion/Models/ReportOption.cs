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
    
    public partial class ReportOption
    {
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> IdEmployee { get; set; }
        public System.Guid IdReport { get; set; }
        public string ReportState { get; set; }
        public int IsDefault { get; set; }
        public string Description { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime Modified { get; set; }
        public long SequenceValue { get; set; }
        public int IsShared { get; set; }
    }
}

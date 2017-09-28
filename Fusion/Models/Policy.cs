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
    
    public partial class Policy
    {
        public Policy()
        {
            this.DepartmentPolicy = new HashSet<DepartmentPolicy>();
            this.OccupationPolicy = new HashSet<OccupationPolicy>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public byte[] Data { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime Modified { get; set; }
        public long SequenceValue { get; set; }
    
        public virtual ICollection<DepartmentPolicy> DepartmentPolicy { get; set; }
        public virtual ICollection<OccupationPolicy> OccupationPolicy { get; set; }
    }
}
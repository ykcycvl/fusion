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
    
    public partial class EntranceRule
    {
        public EntranceRule()
        {
            this.EmployeeGroup = new HashSet<EmployeeGroup>();
            this.EntranceGroup = new HashSet<EntranceGroup>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public int IsActive { get; set; }
        public byte[] Data { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime Modified { get; set; }
        public Nullable<System.Guid> AccessScheduleId { get; set; }
        public long SequenceValue { get; set; }
    
        public virtual AccessSchedule AccessSchedule { get; set; }
        public virtual ICollection<EmployeeGroup> EmployeeGroup { get; set; }
        public virtual ICollection<EntranceGroup> EntranceGroup { get; set; }
    }
}
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
    
    public partial class OccupationSchedule
    {
        public System.Guid Id { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.Guid OccupationId { get; set; }
        public Nullable<System.Guid> ScheduleId { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime Modified { get; set; }
        public long SequenceValue { get; set; }
    
        public virtual Occupation Occupation { get; set; }
        public virtual Schedule Schedule { get; set; }
    }
}

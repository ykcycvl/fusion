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
    
    public partial class WorkRoleRate1
    {
        public System.Guid Id { get; set; }
        public System.Guid IdRole { get; set; }
        public decimal BaseRate { get; set; }
        public decimal OvertimeRate { get; set; }
        public decimal NightRate { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime Modified { get; set; }
        public long SequenceValue { get; set; }
        public int UnitType { get; set; }
        public int BaseRateType { get; set; }
        public decimal WeekendRate { get; set; }
        public decimal HolidayRate { get; set; }
    
        public virtual WorkRole WorkRole { get; set; }
    }
}
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
    
    public partial class EntranceGeoSettings
    {
        public System.Guid Id { get; set; }
        public System.Guid EntranceId { get; set; }
        public long RegistrationType { get; set; }
        public long OrdinalNumber { get; set; }
        public long Color { get; set; }
        public string PointsJSON { get; set; }
        public bool NotUseInTimeAccounting { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime Modified { get; set; }
        public long SequenceValue { get; set; }
    }
}
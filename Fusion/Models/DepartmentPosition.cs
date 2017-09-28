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
    
    public partial class DepartmentPosition
    {
        public DepartmentPosition()
        {
            this.Occupation = new HashSet<Occupation>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid DepartmentId { get; set; }
        public System.Guid PositionId { get; set; }
        public int Quantity { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime Modified { get; set; }
        public long SequenceValue { get; set; }
    
        public virtual Department Department { get; set; }
        public virtual Position Position { get; set; }
        public virtual ICollection<Occupation> Occupation { get; set; }
    }
}
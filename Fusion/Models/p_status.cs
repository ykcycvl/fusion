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
    
    public partial class p_status
    {
        public p_status()
        {
            this.p_task = new HashSet<p_task>();
            this.p_userTask = new HashSet<p_userTask>();
        }
    
        public int id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    
        public virtual ICollection<p_task> p_task { get; set; }
        public virtual ICollection<p_userTask> p_userTask { get; set; }
    }
}

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
    
    public partial class bd_organization
    {
        public bd_organization()
        {
            this.bd_order = new HashSet<bd_order>();
            this.bd_subdivision = new HashSet<bd_subdivision>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
    
        public virtual ICollection<bd_order> bd_order { get; set; }
        public virtual ICollection<bd_subdivision> bd_subdivision { get; set; }
    }
}

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
    
    public partial class bd_measurement
    {
        public bd_measurement()
        {
            this.bd_nomenclature = new HashSet<bd_nomenclature>();
            this.bd_order = new HashSet<bd_order>();
        }
    
        public byte id { get; set; }
        public string name { get; set; }
    
        public virtual ICollection<bd_nomenclature> bd_nomenclature { get; set; }
        public virtual ICollection<bd_order> bd_order { get; set; }
    }
}
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
    
    public partial class bd_employee
    {
        public bd_employee()
        {
            this.bd_order = new HashSet<bd_order>();
        }
    
        public string domain_login { get; set; }
        public string full_name { get; set; }
        public int subdiv_id { get; set; }
    
        public virtual bd_subdivision bd_subdivision { get; set; }
        public virtual ICollection<bd_order> bd_order { get; set; }
    }
}

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
    
    public partial class bd_reclamation
    {
        public int id { get; set; }
        public int restaurant_id { get; set; }
        public System.DateTime date { get; set; }
        public int vendor_id { get; set; }
        public int nomenclature_id { get; set; }
        public int problem_id { get; set; }
        public string solution { get; set; }
        public string filePath { get; set; }
        public Nullable<int> state_id { get; set; }
        public string comment { get; set; }
    
        public virtual bd_nomenclature bd_nomenclature { get; set; }
        public virtual bd_reclamation_problems bd_reclamation_problems { get; set; }
        public virtual bd_subdivision bd_subdivision { get; set; }
        public virtual bd_vendor bd_vendor { get; set; }
        public virtual bd_states bd_states { get; set; }
    }
}

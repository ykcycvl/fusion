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
    
    public partial class bd_rests
    {
        public int id { get; set; }
        public int nomenclature_id { get; set; }
        public int restaurant_id { get; set; }
        public Nullable<int> count { get; set; }
    
        public virtual bd_nomenclature bd_nomenclature { get; set; }
        public virtual bd_subdivision bd_subdivision { get; set; }
    }
}

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
    
    public partial class bd_reclamation_files
    {
        public int id { get; set; }
        public Nullable<int> reclamation_id { get; set; }
        public string file { get; set; }
    
        public virtual bd_reclamation bd_reclamation { get; set; }
    }
}

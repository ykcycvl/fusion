//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fusion.Models.SKK
{
    using System;
    using System.Collections.Generic;
    
    public partial class ActFile
    {
        public int id { get; set; }
        public Nullable<int> act_data_id { get; set; }
        public string filename { get; set; }
    
        public virtual ActData ActData { get; set; }
    }
}
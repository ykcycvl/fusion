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
    
    public partial class ActData
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ActData()
        {
            this.ActFile = new HashSet<ActFile>();
        }
    
        public int id { get; set; }
        public Nullable<int> act_id { get; set; }
        public Nullable<int> article_id { get; set; }
        public Nullable<int> rating { get; set; }
        public Nullable<bool> accord { get; set; }
        public string comment { get; set; }
    
        public virtual Act Act { get; set; }
        public virtual Article Article { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActFile> ActFile { get; set; }
    }
}

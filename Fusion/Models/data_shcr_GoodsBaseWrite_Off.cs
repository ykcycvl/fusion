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
    
    public partial class data_shcr_GoodsBaseWrite_Off
    {
        public System.Guid GUID { get; set; }
        public Nullable<System.Guid> server_guid { get; set; }
        public Nullable<short> Deleted { get; set; }
        public Nullable<System.DateTime> dateinsert { get; set; }
        public Nullable<int> RID { get; set; }
        public Nullable<int> saler_RID { get; set; }
        public string saler_name { get; set; }
        public Nullable<int> store_RID { get; set; }
        public string store_name { get; set; }
    }
}

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
    
    public partial class data_shcr_PLDoc
    {
        public System.Guid GUID { get; set; }
        public Nullable<System.Guid> server_guid { get; set; }
        public Nullable<short> Deleted { get; set; }
        public Nullable<System.DateTime> dateinsert { get; set; }
        public Nullable<int> RID { get; set; }
        public string code_text { get; set; }
        public Nullable<int> code_numb { get; set; }
        public Nullable<int> type { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<int> corr_rid { get; set; }
        public Nullable<int> curr_rid { get; set; }
        public Nullable<int> options { get; set; }
        public string corr_name { get; set; }
        public string curr_code { get; set; }
        public string note { get; set; }
        public string create_user { get; set; }
    }
}
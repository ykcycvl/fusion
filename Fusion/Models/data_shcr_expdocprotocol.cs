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
    
    public partial class data_shcr_expdocprotocol
    {
        public System.Guid GUID { get; set; }
        public Nullable<System.Guid> server_guid { get; set; }
        public string hash { get; set; }
        public Nullable<System.DateTime> dateinsert { get; set; }
        public Nullable<int> doc_rid { get; set; }
        public string numb_code { get; set; }
        public Nullable<int> text_code { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<int> options { get; set; }
        public string edit_user { get; set; }
        public Nullable<double> edit_datetime { get; set; }
        public string create_user { get; set; }
        public Nullable<double> create_datetime { get; set; }
        public string del_user { get; set; }
        public Nullable<double> del_datetime { get; set; }
    }
}

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
    
    public partial class MSG_OUTGOINGS
    {
        public long OUTGOING_ID { get; set; }
        public Nullable<short> MESSAGE_TYPE { get; set; }
        public Nullable<System.DateTime> DATE_FROM { get; set; }
        public Nullable<System.DateTime> DATE_TO { get; set; }
        public Nullable<short> STATUS { get; set; }
        public Nullable<long> PEOPLE_ID { get; set; }
        public Nullable<int> CLIENT_ID { get; set; }
        public Nullable<int> FIELD_ID { get; set; }
        public Nullable<long> VALUE_ID { get; set; }
        public Nullable<long> FIELD_EDIT { get; set; }
        public Nullable<int> USER_ID { get; set; }
        public string MESSAGE_TEXT { get; set; }
        public Nullable<int> OPERATION_PARAM_ID { get; set; }
    }
}

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
    
    public partial class CARD_CONTACTS
    {
        public long CONTACT_ID { get; set; }
        public Nullable<long> PEOPLE_ID { get; set; }
        public Nullable<short> DELETED { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<int> CONTACT_TYPE { get; set; }
        public Nullable<int> FLAGS { get; set; }
        public System.Guid GUID { get; set; }
        public System.Guid TRANSACT_GUID { get; set; }
        public string CONTACT_NOTES { get; set; }
        public string CONTACT_VALUE { get; set; }
        public string CONTACT_VALUE_A { get; set; }
    }
}

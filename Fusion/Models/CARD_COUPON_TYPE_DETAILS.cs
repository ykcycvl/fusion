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
    
    public partial class CARD_COUPON_TYPE_DETAILS
    {
        public int COUPON_TYPE_DETAIL_ID { get; set; }
        public Nullable<int> COUPON_TYPE_ID { get; set; }
        public Nullable<int> PARENT_ID { get; set; }
        public Nullable<int> FLAGS { get; set; }
        public Nullable<short> DELETED { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<decimal> COUPON_VALUE { get; set; }
        public Nullable<int> COUNT_MAX { get; set; }
        public Nullable<int> PRIORITY { get; set; }
        public System.Guid GUID { get; set; }
        public System.Guid TRANSACT_GUID { get; set; }
        public Nullable<int> EXTERNAL_ID { get; set; }
        public Nullable<decimal> DISCOUNT_LIMIT { get; set; }
        public Nullable<int> CLIENT_ID { get; set; }
        public string NAME { get; set; }
        public string EXTERNAL_CODE { get; set; }
        public string NOTES { get; set; }
    }
}

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
    
    public partial class CARD_COUPON_TYPES
    {
        public int COUPON_TYPE_ID { get; set; }
        public Nullable<int> PARENT_ID { get; set; }
        public Nullable<int> DISCOUNT_ID { get; set; }
        public Nullable<int> BONUS_ID { get; set; }
        public Nullable<int> SUMM_ID { get; set; }
        public Nullable<int> AMOUNT { get; set; }
        public Nullable<int> FLAGS { get; set; }
        public Nullable<short> DELETED { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public System.Guid GUID { get; set; }
        public System.Guid TRANSACT_GUID { get; set; }
        public string NAME { get; set; }
        public string NOTES { get; set; }
        public Nullable<System.DateTime> SUBSCRIBE_DATE_FROM { get; set; }
        public Nullable<System.DateTime> SUBSCRIBE_DATE_TO { get; set; }
        public Nullable<int> TIME_ID { get; set; }
        public Nullable<int> POOL_COUNT { get; set; }
        public Nullable<int> POOL_COUNT_INC { get; set; }
        public Nullable<int> POOL_PREFIX { get; set; }
        public Nullable<short> ACTION_ID { get; set; }
        public Nullable<short> ACTION_COUNT { get; set; }
        public Nullable<decimal> SUMM { get; set; }
        public Nullable<short> COUPON_CLASS { get; set; }
        public Nullable<int> DELAY { get; set; }
    }
}

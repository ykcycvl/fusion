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
    
    public partial class STAT_RK7_SHIFTS_PARAMS
    {
        public System.Guid GLOBALSHIFTGUID { get; set; }
        public Nullable<System.Guid> RESTAURANTGUID { get; set; }
        public Nullable<System.Guid> WAITERGUID { get; set; }
        public Nullable<System.Guid> CURRENCYGUID { get; set; }
        public Nullable<System.Guid> MIDSERVERGUID { get; set; }
        public Nullable<System.Guid> STATIONGUID { get; set; }
        public string RESTAURANT { get; set; }
        public string WAITER { get; set; }
        public string CURRENCY { get; set; }
        public string MIDSERVER { get; set; }
        public string STATION { get; set; }
        public Nullable<System.DateTime> GLOBALSHIFTLOGICDATE { get; set; }
        public Nullable<System.DateTime> GLOBALSHIFTSTART { get; set; }
        public Nullable<System.DateTime> GLOBALSHIFTEND { get; set; }
        public Nullable<short> ISFISC { get; set; }
        public Nullable<decimal> BASICSUM { get; set; }
        public Nullable<decimal> NATIONALSUM { get; set; }
        public Nullable<int> CHECKCOUNT { get; set; }
        public Nullable<int> ORDERCOUNT { get; set; }
        public Nullable<decimal> ORIGINALSUM { get; set; }
    }
}

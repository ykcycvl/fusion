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
    
    public partial class VRK7CMZREPORTS
    {
        public string RESTAURANTGUID { get; set; }
        public string CREATORGUID { get; set; }
        public string CREATORNAME { get; set; }
        public string DEVICEGUID { get; set; }
        public string DEVICENAME { get; set; }
        public string CASHSTATIONGUID { get; set; }
        public string CASHSTATIONNAME { get; set; }
        public Nullable<int> REPORTNUM { get; set; }
        public Nullable<decimal> STARTCOUNTER { get; set; }
        public Nullable<decimal> ENDCOUNTER { get; set; }
        public Nullable<decimal> TOTALRECEIPTS { get; set; }
        public Nullable<decimal> CASH { get; set; }
        public Nullable<decimal> CARDS { get; set; }
        public Nullable<decimal> OTHER { get; set; }
        public Nullable<decimal> RETURNS { get; set; }
        public Nullable<decimal> ADVANCES { get; set; }
        public Nullable<decimal> PROCEEDS { get; set; }
        public Nullable<System.DateTime> STARTDATETIME { get; set; }
        public Nullable<System.DateTime> ENDDATETIME { get; set; }
        public int MIDSERVER { get; set; }
        public string MIDSERVERGUID { get; set; }
        public string MIDSERVERNAME { get; set; }
        public int SHIFTNUM { get; set; }
        public int ZREPORTID { get; set; }
        public string FISCID { get; set; }
        public Nullable<System.DateTime> SHIFTDATE { get; set; }
    }
}

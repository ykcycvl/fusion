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
    
    public partial class TAXPARTS
    {
        public int VISIT { get; set; }
        public int MIDSERVER { get; set; }
        public Nullable<int> ORDERIDENT { get; set; }
        public int BINDINGUNI { get; set; }
        public Nullable<int> UNI { get; set; }
        public int SIFR { get; set; }
        public Nullable<decimal> BASEFORTAX { get; set; }
        public Nullable<double> TAXRATE { get; set; }
        public Nullable<short> TAXFLAGS { get; set; }
        public Nullable<decimal> SUM { get; set; }
        public Nullable<decimal> NATIONALSUM { get; set; }
        public string TRANSACT_GUID { get; set; }
        public Nullable<int> DBSTATUS { get; set; }
    }
}

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
    
    public partial class TEMPVISITS
    {
        public int SIFR { get; set; }
        public int MIDSERVER { get; set; }
        public Nullable<int> CREATOR { get; set; }
        public Nullable<int> GUESTTYPE { get; set; }
        public Nullable<int> STARTGUESTCNT { get; set; }
        public Nullable<int> GUESTCNT { get; set; }
        public Nullable<int> RESERVSOURCE { get; set; }
        public Nullable<int> RESERVID { get; set; }
        public Nullable<int> RESERVDURATION { get; set; }
        public Nullable<int> RESERVSTATE { get; set; }
        public Nullable<int> RESERVFLAGS { get; set; }
        public Nullable<int> ENTRCARDMODE { get; set; }
        public Nullable<decimal> MAXCREDITSUM { get; set; }
        public Nullable<System.DateTime> STARTTIME { get; set; }
        public Nullable<System.DateTime> QUITTIME { get; set; }
        public Nullable<short> FINISHED { get; set; }
        public string VISITEXTRAINFOTOSAVE { get; set; }
        public byte[] SEATSCLOSED { get; set; }
        public string VISITOTHEREXTRAINFO { get; set; }
        public string HOLDER { get; set; }
        public Nullable<int> ISTARTCOMMONSHIFT { get; set; }
        public Nullable<int> IQUITCOMMONSHIFT { get; set; }
        public string GUIDSTRING { get; set; }
        public Nullable<int> UNICOUNT { get; set; }
        public string TRANSACT_GUID { get; set; }
        public Nullable<int> TEMPDATAKIND { get; set; }
        public Nullable<int> TEMPDATASIGN { get; set; }
        public Nullable<int> TEMPSERVERID { get; set; }
    }
}

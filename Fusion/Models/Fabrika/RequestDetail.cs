//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fusion.Models.Fabrika
{
    using System;
    using System.Collections.Generic;
    
    public partial class RequestDetail
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public int RequestID { get; set; }
        public int RestaurantID { get; set; }
        public int ProductID { get; set; }
        public decimal Quantity { get; set; }
        public string UserName { get; set; }
    
        public virtual Nomenclature Nomenclature { get; set; }
        public virtual Request Request { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ZakupEntities3 : DbContext
    {
        public ZakupEntities3()
            : base("name=ZakupEntities3")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<bd_category> bd_category { get; set; }
        public virtual DbSet<bd_employee> bd_employee { get; set; }
        public virtual DbSet<bd_measurement> bd_measurement { get; set; }
        public virtual DbSet<bd_nomenclature> bd_nomenclature { get; set; }
        public virtual DbSet<bd_order> bd_order { get; set; }
        public virtual DbSet<bd_organization> bd_organization { get; set; }
        public virtual DbSet<bd_subdivision> bd_subdivision { get; set; }
        public virtual DbSet<bd_vendor> bd_vendor { get; set; }
        public virtual DbSet<bd_states> bd_states { get; set; }
    }
}

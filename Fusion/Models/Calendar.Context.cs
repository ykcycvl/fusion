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
    
    public partial class CalendarEntities : DbContext
    {
        public CalendarEntities()
            : base("name=CalendarEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<mc_extra> mc_extra { get; set; }
        public virtual DbSet<mc_task> mc_task { get; set; }
        public virtual DbSet<mc_userTask> mc_userTask { get; set; }
    }
}

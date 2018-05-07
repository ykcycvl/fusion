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
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<ArticleCategory> ArticleCategory { get; set; }
        public virtual DbSet<bd_category> bd_category { get; set; }
        public virtual DbSet<bd_employee> bd_employee { get; set; }
        public virtual DbSet<bd_measurement> bd_measurement { get; set; }
        public virtual DbSet<bd_nomenclature> bd_nomenclature { get; set; }
        public virtual DbSet<bd_nomenclature_state> bd_nomenclature_state { get; set; }
        public virtual DbSet<bd_order> bd_order { get; set; }
        public virtual DbSet<bd_organization> bd_organization { get; set; }
        public virtual DbSet<bd_reclamation> bd_reclamation { get; set; }
        public virtual DbSet<bd_reclamation_files> bd_reclamation_files { get; set; }
        public virtual DbSet<bd_reclamation_problems> bd_reclamation_problems { get; set; }
        public virtual DbSet<bd_rests> bd_rests { get; set; }
        public virtual DbSet<bd_states> bd_states { get; set; }
        public virtual DbSet<bd_subdivision> bd_subdivision { get; set; }
        public virtual DbSet<bd_vendor> bd_vendor { get; set; }
        public virtual DbSet<bitrixUser> bitrixUser { get; set; }
        public virtual DbSet<CRMSegment> CRMSegment { get; set; }
        public virtual DbSet<CRMSended> CRMSended { get; set; }
        public virtual DbSet<CRMToSend> CRMToSend { get; set; }
        public virtual DbSet<DLVOrder> DLVOrder { get; set; }
        public virtual DbSet<DLVOrderLog> DLVOrderLog { get; set; }
        public virtual DbSet<EmailStatus> EmailStatus { get; set; }
        public virtual DbSet<EmailTemplate> EmailTemplate { get; set; }
        public virtual DbSet<host> host { get; set; }
        public virtual DbSet<iptable> iptable { get; set; }
        public virtual DbSet<Mail> Mail { get; set; }
        public virtual DbSet<mc_extra> mc_extra { get; set; }
        public virtual DbSet<mc_task> mc_task { get; set; }
        public virtual DbSet<mc_userTask> mc_userTask { get; set; }
        public virtual DbSet<ortus_log> ortus_log { get; set; }
        public virtual DbSet<p_status> p_status { get; set; }
        public virtual DbSet<p_task> p_task { get; set; }
        public virtual DbSet<p_taskChecklist> p_taskChecklist { get; set; }
        public virtual DbSet<p_userTask> p_userTask { get; set; }
        public virtual DbSet<sb_managers> sb_managers { get; set; }
        public virtual DbSet<sb_problems> sb_problems { get; set; }
        public virtual DbSet<sb_restaurants> sb_restaurants { get; set; }
        public virtual DbSet<sb_rights> sb_rights { get; set; }
        public virtual DbSet<sb_top> sb_top { get; set; }
        public virtual DbSet<sber> sber { get; set; }
        public virtual DbSet<server> server { get; set; }
        public virtual DbSet<stoplist> stoplist { get; set; }
        public virtual DbSet<storage> storage { get; set; }
        public virtual DbSet<VegaPersonalSetting> VegaPersonalSetting { get; set; }
        public virtual DbSet<VegaSetting> VegaSetting { get; set; }
        public virtual DbSet<hostip> hostip { get; set; }
        public virtual DbSet<Issue> Issue { get; set; }
    }
}

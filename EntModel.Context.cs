﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AngApp
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OVODEntities5 : DbContext
    {
        public OVODEntities5()
            : base("name=OVODEntities5")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<VW_COUNTRY1> VW_COUNTRY1 { get; set; }
        public virtual DbSet<TTMAST> TTMASTs { get; set; }
        public virtual DbSet<DEPARTMENT> DEPARTMENTs { get; set; }
        public virtual DbSet<VW_EMPLOYEE> VW_EMPLOYEE { get; set; }
        public virtual DbSet<DESIGNATION> DESIGNATIONs { get; set; }
        public virtual DbSet<EMPLOYEESAL> EMPLOYEESALs { get; set; }
        public virtual DbSet<EMPLOYEE> EMPLOYEEs { get; set; }
        public virtual DbSet<EMPSAL> EMPSALs { get; set; }
        public virtual DbSet<VW_HD_HRPAYADJ> VW_HD_HRPAYADJ { get; set; }
        public virtual DbSet<VW_DT_HRPAYADJ> VW_DT_HRPAYADJ { get; set; }
        public virtual DbSet<DT_INVOICE> DT_INVOICE { get; set; }
        public virtual DbSet<HD_INVOICE> HD_INVOICE { get; set; }
        public virtual DbSet<DT_HRPAYADJ> DT_HRPAYADJ { get; set; }
        public virtual DbSet<HD_HRPAYADJ> HD_HRPAYADJ { get; set; }
        public virtual DbSet<Rak> Raks { get; set; }
        public virtual DbSet<Shelf> Shelves { get; set; }
        public virtual DbSet<Emp_Doc_History> Emp_Doc_History { get; set; }
        public virtual DbSet<ANG_EMPLOYEE> ANG_EMPLOYEE { get; set; }
        public virtual DbSet<Doc_History> Doc_History { get; set; }
        public virtual DbSet<DIAGNOSI> DIAGNOSIS { get; set; }
        public virtual DbSet<DRUG> DRUGs { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<DT_LEAVE> DT_LEAVE { get; set; }
    }
}
﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EJOLEntitiesNew : DbContext
    {
        public EJOLEntitiesNew()
            : base("name=EJOLEntitiesNew")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<vw_Pendaftaran> vw_Pendaftaran { get; set; }
        public virtual DbSet<vw_EJOLSchedule> vw_EJOLSchedule { get; set; }
    }
}
﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MITD.PMS.Integration.Data.EF.DBModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PersonnelSoft2005Entities : DbContext
    {
        public PersonnelSoft2005Entities()
            : base("name=PersonnelSoft2005Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<affiliateCompany> affiliateCompanies { get; set; }
        public virtual DbSet<JOBSTitle> JOBSTitles { get; set; }
        public virtual DbSet<OrganTreeNodeType> OrganTreeNodeTypes { get; set; }
        public virtual DbSet<Rasteh> Rastehs { get; set; }
        public virtual DbSet<Reshteh> Reshtehs { get; set; }
        public virtual DbSet<VW_OrganTree> VW_OrganTree { get; set; }
    }
}

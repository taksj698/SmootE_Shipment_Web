using SmootE_Shipment_Web.Core.dbModels;
using SmootE_Shipment_Web.Core.dbStored;
using Microsoft.Ajax.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SmootE_Shipment_Web.Data.Repository.SQLServer
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PRResult>().HasNoKey();
        }

        public DbSet<TB_User> TB_User { get; set; }
        public DbSet<TB_Package> TB_Package { get; set; }
        public DbSet<TB_PackingHeader> TB_PackingHeader { get; set; }
        public DbSet<TB_PackingDetails> TB_PackingDetails { get; set; }

        public DbSet<TB_Country> TB_Country { get; set; }
        public DbSet<TB_IPODetails> TB_IPODetails { get; set; }
        public DbSet<TB_IPOHeader> TB_IPOHeader { get; set; }
    }
}

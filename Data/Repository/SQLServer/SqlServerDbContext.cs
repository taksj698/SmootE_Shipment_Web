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
        public DbSet<TB_Customers> TB_Customers { get; set; }        
        public DbSet<TB_WeightData> TB_WeightData { get; set; }
        public DbSet<TB_QualityTransaction> TB_QualityTransaction { get; set; }
        public DbSet<TB_DocumentFile> TB_DocumentFile { get; set; }
        public DbSet<TB_VisualConfigs> TB_VisualConfigs { get; set; }
        public DbSet<TB_Branch> TB_Branch { get; set; }    
        public DbSet<TB_WeightType> TB_WeightType { get; set; }
    }
}

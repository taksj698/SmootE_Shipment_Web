using QuickVisualWebWood.Core.dbModels;
using QuickVisualWebWood.Core.dbStored;
using Microsoft.Ajax.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace QuickVisualWebWood.Data.Repository.SQLServer
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PRResult>().HasNoKey();
        }

        public DbSet<TB_Users> TB_Users { get; set; }
        public DbSet<TB_Customers> TB_Customers { get; set; }        
        public DbSet<TB_WeightData> TB_WeightData { get; set; }
        public DbSet<TB_QualityTransaction> TB_QualityTransaction { get; set; }
        public DbSet<TB_DocumentFile> TB_DocumentFile { get; set; }
        public DbSet<TB_VisualConfigs> TB_VisualConfigs { get; set; }
    }
}

using SmootE_Shipment_Web.Core.dbModels;
using SmootE_Shipment_Web.Core.dbStored;
using Microsoft.Ajax.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SmootE_Shipment_Web.Data.Repository.SQLServer
{
    public class SqlServerDbContext2 : DbContext
    {
        public SqlServerDbContext2(DbContextOptions<SqlServerDbContext2> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        public DbSet<TB_Log> TB_Log { get; set; }
    }
}

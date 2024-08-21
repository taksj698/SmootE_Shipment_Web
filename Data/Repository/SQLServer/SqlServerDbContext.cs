using Document_Control.Core.dbModels;
using Document_Control.Core.dbStored;
using Microsoft.Ajax.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Document_Control.Data.Repository.SQLServer
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
		public DbSet<TbApprovalMatrix> TbApprovalMatrix { get; set; }
		public DbSet<TbCompany> TbCompany { get; set; }
		public DbSet<TbConfigs> TbConfigs { get; set; }
		public DbSet<TbDivision> TbDivision { get; set; }
		public DbSet<TbDocumentTransaction> TbDocumentTransaction { get; set; }
		public DbSet<TbPosition> TbPosition { get; set; }
		public DbSet<TbPriority> TbPriority { get; set; }
		public DbSet<TbUser> TbUser { get; set; }
		public DbSet<TbApprovalTransaction> TbApprovalTransaction { get; set; }
		public DbSet<TbHistoryTransaction> TbHistoryTransaction { get; set; }
		public DbSet<TbStatus> TbStatus { get; set; }
		public DbSet<TbDocumentFile> TbDocumentFile { get; set; }
		public DbSet<TbRole> TbRole { get; set; }
		public DbSet<PRResult> PRResult { get; set; }	
	}
}

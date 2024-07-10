using Document_Control.Core.dbModels;
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



		public DbSet<TbApprovalMatrix> TbApprovalMatrix { get; set; }
		public DbSet<TbCompany> TbCompany { get; set; }
		public DbSet<TbConfigs> TbConfigs { get; set; }
		public DbSet<TbDivision> TbDivision { get; set; }
		public DbSet<TbDocumentTransaction> TbDocumentTransaction { get; set; }
		public DbSet<TbPosition> TbPosition { get; set; }
		public DbSet<TbPriority> TbPriority { get; set; }
		public DbSet<TbUser> TbUser { get; set; }
	}
}

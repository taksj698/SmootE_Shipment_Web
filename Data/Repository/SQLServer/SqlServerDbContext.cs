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

		public DbSet<TbUser> TbUser { get; set; }
	}
}

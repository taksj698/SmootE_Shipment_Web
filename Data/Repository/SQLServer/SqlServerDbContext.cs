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

		//public DbSet<postalBoxes> postalBoxes { get; set; }
		//public DbSet<shippingInformation> shippingInformation { get; set; }
		//public DbSet<shippingProvider> shippingProvider { get; set; }
		//public DbSet<user> user { get; set; }
		//public DbSet<userVerify> userVerify { get; set; }
	}
}

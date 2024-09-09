using QuickVisualWebWood.Configs.Options;
using QuickVisualWebWood.Data.Repository.SQLServer;
using Microsoft.EntityFrameworkCore;

namespace QuickVisualWebWood.Configs.Configurations
{
	public static class ConfigSqlServer
	{
		public static void AddSqlServerConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration["Database:SqlServer:ConnectionString"];

			services.Configure<SqlServerOption>(option =>
			{
				option.ConnectionString = connectionString;
			});

			services.AddDbContext<SqlServerDbContext>(option => option
				.UseSqlServer(connectionString)
			//.EnableSensitiveDataLogging()
			);
		}
	}
}

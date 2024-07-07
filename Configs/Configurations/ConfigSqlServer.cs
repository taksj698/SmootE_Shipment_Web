using Document_Control.Configs.Options;
using Document_Control.Data.Repository.SQLServer;
using Microsoft.EntityFrameworkCore;

namespace Document_Control.Configs.Configurations
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

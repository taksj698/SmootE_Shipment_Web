using QuickVisualWebWood.Configs.Options;
using QuickVisualWebWood.Data.Repository.SQLServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace QuickVisualWebWood.Configs.Configurations
{
	public static class ConfigSqlServer
	{
		public static void AddSqlServerConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
            // Get connection strings from the configuration
            var connectionString = configuration["Database:SqlServer:ConnectionString"];
            var connectionString2 = configuration["Database:SqlServer:ConnectionString2"];

            // Configure SqlServerOption for the main database connection
            services.Configure<SqlServerOption>(option =>
            {
                option.ConnectionString = connectionString;
            });

            // Register SqlServerDbContext with the main connection string
            services.AddDbContext<SqlServerDbContext>(options =>
                options.UseSqlServer(connectionString)
            //.EnableSensitiveDataLogging()  // Uncomment if needed
            );

            // Register SqlServerDbContext2 with the second connection string
            services.AddDbContext<SqlServerDbContext2>(options =>
                options.UseSqlServer(connectionString2)
            //.EnableSensitiveDataLogging()  // Uncomment if needed
            );
        }
	}
}

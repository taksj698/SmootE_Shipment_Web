using SmootE_Shipment_Web.Configs.Options;
using SmootE_Shipment_Web.Data.Repository.SQLServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SmootE_Shipment_Web.Configs.Configurations
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
                options.UseSqlServer(connectionString),
                ServiceLifetime.Scoped);

            // Register SqlServerDbContext2 with the second connection string
            services.AddDbContext<SqlServerDbContext2>(options =>
            options.UseSqlServer(connectionString2),
            ServiceLifetime.Scoped);
        }
    }
}

namespace SmootE_Shipment_Web.Configs.Configurations
{
    public static class EnvelopmentConfiguration
    {
        public static WebApplicationBuilder ConfigureEnvelopment(this WebApplicationBuilder builder)
        {
            builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            return builder;
        }
    }
}

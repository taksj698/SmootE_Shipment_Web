using Microsoft.AspNetCore.Authentication.Cookies;

namespace Document_Control.Configs.Configurations
{
    public static class ConfigAuth
    {
        public static void AddAuthConfiguration(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login";
                });

        }
    }
}

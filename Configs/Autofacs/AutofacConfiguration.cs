using Autofac.Extensions.DependencyInjection;
using Autofac;
using SmootE_Shipment_Web.Configs.Configurations;

namespace SmootE_Shipment_Web.Configs.Autofacs
{
	public static class AutofacConfiguration
	{
		public static WebApplicationBuilder ConfigureAutofac(this WebApplicationBuilder builder)
		{
			builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
						.ConfigureContainer<ContainerBuilder>(builder =>
						{
							builder.RegisterModule(new RegisterInstant());
						});
			return builder;
		}
	}
}

using System.Reflection;
using Autofac;
using QuickVisualWebWood.Configs.Extensions;
using QuickVisualWebWood.Configs.Options;

namespace QuickVisualWebWood.Configs.Configurations
{
    public class RegisterInstant : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            const StringComparison stringCompare = StringComparison.CurrentCultureIgnoreCase;
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.Name.EndsWith("Services", stringCompare) ||
						    x.Name.EndsWith("Repository", stringCompare) ||
							x.Name.EndsWith("Business", stringCompare) ||
                            x.Name.EndsWith("Helper", stringCompare))
                .InstancePerLifetimeScope();

			builder.Register(context =>
			{
				var configuration = context.Resolve<IConfiguration>();
				var options = configuration.GetOptions<SqlServerOption>("Database:SqlServer");
				return options;
			}).SingleInstance();

		}
    }
}

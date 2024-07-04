using System.Reflection;
using Autofac;

namespace Document_Control.Configs.Configurations
{
    public class RegisterInstant : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            const StringComparison stringCompare = StringComparison.CurrentCultureIgnoreCase;
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.Name.EndsWith("Services", stringCompare) ||
                            x.Name.EndsWith("Business", stringCompare) ||
                            x.Name.EndsWith("Helper", stringCompare))
                .InstancePerLifetimeScope();

        }
    }
}

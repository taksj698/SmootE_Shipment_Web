namespace SmootE_Shipment_Web.Configs.Extensions
{
	public static class Extension
	{
		public static T GetOptions<T>(this IConfiguration configuration, string section) where T : new()
		{
			var model = new T();
			configuration.GetSection(section).Bind(model);
			return model;
		}
	}
}

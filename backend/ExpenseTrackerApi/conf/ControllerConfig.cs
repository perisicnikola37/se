namespace ExpenseTrackerApi.conf;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

public static class ControllerConfig
{
	public static void ConfigureControllers(this IServiceCollection services)
	{
		services.AddControllers().AddNewtonsoftJson(options =>
		{
			options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
		});
	}
}

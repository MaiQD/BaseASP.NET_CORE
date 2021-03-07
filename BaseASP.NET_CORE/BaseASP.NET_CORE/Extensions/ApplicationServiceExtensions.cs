using BaseASP.NET_CORE.Data;
using BaseASP.NET_CORE.Services.Implement;
using BaseASP.NET_CORE.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BaseASP.NET_CORE.Extensions
{
	public static class ApplicationServiceExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration _config)
		{
			//dependency injection
			services.AddScoped<ILogger, DefaultLogger>();
			services.AddScoped<IWorkContext, WebWorkContext>();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));


			services.AddDbContext<DataContext>(options =>
			{
				options.UseSqlServer(_config.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging();
			});
			return services;
		}
	}
}
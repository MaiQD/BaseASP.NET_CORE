using BaseASP.NET_CORE.Extensions;
using BaseASP.NET_CORE.Middleware;
using BaseASP.NET_CORE.Services.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BaseASP.NET_CORE
{
	public class Startup
	{
		public IConfiguration _configuration { get; }
		private readonly IHostingEnvironment _hostingEnvironment;

		public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
		{
			_configuration = configuration;
			_hostingEnvironment = hostingEnvironment;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});
			services.AddApplicationServices(_configuration);
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddSessionStateTempDataProvider();
			services.AddSession();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger logger)
		{
			app.UseMyExceptionHandler(_hostingEnvironment);
			app.UseStaticFiles();
			app.UseCookiePolicy();
			app.UseSession();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
				routes.MapRoute(
					name: "areas",
					template: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
				  );
			});
		}
	}
}
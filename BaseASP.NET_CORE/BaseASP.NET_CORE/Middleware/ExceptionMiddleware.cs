using BaseASP.NET_CORE.Services.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Threading.Tasks;

namespace BaseASP.NET_CORE.Middleware
{
	public static class ExceptionMiddleware
	{
		/// <summary>
		/// Add exception handling
		/// </summary>
		/// <param name="application">Builder for configuring an application's request pipeline</param>
		public static void UseMyExceptionHandler(this IApplicationBuilder application, IHostingEnvironment hostingEnvironment)
		{
			var useDetailedExceptionPage = hostingEnvironment.IsDevelopment();
			if (useDetailedExceptionPage)
			{
				//get detailed exceptions for developing and testing purposes
				application.UseDeveloperExceptionPage();
			}
			else
			{
				//or use special exception handler
				application.UseExceptionHandler("/Home/Error");
			}
			//log errors
			application.UseExceptionHandler(handler =>
			{
				handler.Run(context =>
				{
					var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
					if (exception == null)
						return Task.CompletedTask;
					try
					{
						//log error
					}
					catch (Exception ex)
					{
						var err = ex;
					}
					finally
					{
						//rethrow the exception to show the error page
						throw exception;
					}
				});
			});
		}
	}
}
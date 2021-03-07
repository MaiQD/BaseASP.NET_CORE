using BaseASP.NET_CORE.Entities.HeThong;
using BaseASP.NET_CORE.Extensions;
using BaseASP.NET_CORE.Models;
using BaseASP.NET_CORE.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BaseASP.NET_CORE.Controllers
{
	public class HomeController : BaseController
	{
		private readonly ILogger _logger;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public HomeController(ILogger logger,
			IHttpContextAccessor httpContextAccessor)
		{
			_logger = logger;
			_httpContextAccessor = httpContextAccessor;
		}

		public IActionResult Index()
		{
			_httpContextAccessor.HttpContext.Session.SetObject("CurrentCustomer", new NguoiDung());
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
using Microsoft.AspNetCore.Mvc;

namespace BaseASP.NET_CORE.Areas.Admin.Controllers
{
	public class AdminController : BaseAdminController
	{
		public AdminController()
		{
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Login()
		{
			return View();
		}
	}
}
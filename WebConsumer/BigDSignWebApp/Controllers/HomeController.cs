using BigDSignWebApp.Cookies;
using BigDSignWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BigDSignWebApp.Controllers
{
	public class HomeController : BaseController
	{
		public HomeController(ICookieProvider cookieProvider) : base(cookieProvider)
		{
			
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult PrivacyPolicy()
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

using BigDSignWebApp.Cookies;
using BusinessLogicLayer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModelLayer;
using ServiceLayer;
using System;
using System.Security.Claims;

namespace BigDSignWebApp.Controllers
{
	public class AuthController : BaseController
	{
		private readonly IUserLogic _userLogic;
		private readonly ILogger<HomeController> _logger;

		public AuthController(ICookieProvider cookieProvider, IUserLogic userLogic, ILogger<HomeController> logger) : base(cookieProvider)
		{
			_userLogic = userLogic;
			_logger = logger;
		}

		// GET: /Auth/Account
		[Authorize]
		public IActionResult Account()
		{
			return View();
		}

		// GET: /Auth/Login
		public IActionResult Login()
		{
			return View();
		}

		// POST: /Auth/Login
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login([FromForm] string username, [FromForm] string password, [FromForm] bool rememberMe, [FromQuery] string? returnUrl = null)
		{
			try
			{
				User? user = await _userLogic.AuthenticateUser(username, password);
				// If the user was successfully authenticated.
				if (user is not null && user.UserRole is not null)
				{
					await CookieProvider.Login(user, rememberMe);
					_logger.LogInformation("[{Time}] User \"{Username}\" logged in", DateTime.UtcNow, user.Username);
					if (returnUrl == null)
					{
						// Redirect to account page.
						return RedirectToAction(nameof(Account));
					}
					else
					{
						// Redirect to return URL.
						return LocalRedirect(returnUrl);
					}
				}
				else
				{
					ShowAlert("danger", "500 Internal server error.");
					return View();
				}
			}
			// Catch exceptions returned from the API service.
			catch (ServiceException exception)
			{
				ShowAlert("danger", $"{(int)exception.StatusCode} {exception.Message}");
				return View();
			}
			// Catch other exceptions.
			catch (Exception exception)
			{
				Console.Error.Write(exception);
				ShowAlert("danger", $"500 Internal server error.");
				return View();
			}
		}

		// TODO: Use POST instead.
		// GET: /Auth/Logout
		public async Task<IActionResult> Logout()
		{
			CookieUser? cookieUser = CookieProvider.GetCookieUser();
			if (cookieUser is not null)
			{
				await CookieProvider.Logout();
				_logger.LogInformation("[{Time}] User \"{Username}\" logged out", DateTime.UtcNow, cookieUser.Username);
			}
			// Redirect to login page.
			return RedirectToAction(nameof(Login));
		}

		// GET: /Auth/Register
		public IActionResult Register()
		{
			return View();
		}

		// POST: /Auth/Register
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register([FromForm] string firstName, [FromForm] string lastName, [FromForm] string email, [FromForm] string username, [FromForm] string password)
		{
			try
			{
				User user = new User(-1, firstName, lastName, email, username, password);
				await _userLogic.RegisterUser(user);
				_logger.LogInformation("[{Time}] User \"{Username}\" registered", DateTime.UtcNow, username);
				// Redirect to login page.
				return RedirectToAction(nameof(Login));
			}
			// Catch exceptions returned from the API service.
			catch (ServiceException exception)
			{
				ShowAlert("danger", $"{(int)exception.StatusCode} {exception.Message}");
				return View();
			}
			// Catch other exceptions.
			catch (Exception exception)
			{
				Console.Error.Write(exception);
				ShowAlert("danger", $"500 Internal server error.");
				return View();
			}
		}
	}
}

using EmployeesApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using EmployeesApp.Core.Services;

namespace EmployeesApp.Controllers
{
	public class AccountController : Controller
	{
		private readonly IAccountService _service;

		public AccountController(IAccountService service)
		{
			_service = service;
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var success = await _service.AuthenticateAsync(model.Email, model.Password);
				if (success)
				{
					await Authenticate(model.Email);

					return RedirectToAction("Index", "Employees");
				}

				ModelState.AddModelError("", "Incorrect login or password.");
			}

			return View(model);
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var success = await _service.RegisterAsync(model.Email, model.Password);
				if (success)
				{
					await Authenticate(model.Email);
					return RedirectToAction("Index", "Employees");
				}
				else
					ModelState.AddModelError("", "Existing email address.");
			}

			return View(model);
		}

		private async Task Authenticate(string? userName)
		{
            ArgumentNullException.ThrowIfNull(userName);

            var claims = new List<Claim>
			{
				new(ClaimsIdentity.DefaultNameClaimType, userName)
			};

			var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "Account");
		}
	}
}

using System.Security.Claims;
using _5BB_POS.Models;
using _5BB_POS.RequestModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _5BB_POS.Controllers
{
	public class RegisterController : Controller
	{
		private readonly SimplePosContext _context;

		public RegisterController(SimplePosContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(RegisterRequestModel request)
		{
			var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
			if (!ModelState.IsValid)
			{
				return View(request);
			}
			var existingUser = await _context.TblUsers.AnyAsync(u => u.Email == request.Email);
			if (existingUser)
			{
				ModelState.AddModelError("Email", "Email is already in use.");
				return View();
			}

			var newUser = new TblUser
			{
				FullName = request.FullName,
				Email = request.Email,
				Password = hashPassword,
				Role= request.Role,
			};
			_context.TblUsers.Add(newUser);
			await _context.SaveChangesAsync();
			var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, newUser.FullName),
			new Claim(ClaimTypes.Email, newUser.Email),
			new Claim(ClaimTypes.Role, newUser.Role),
		};

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
			return RedirectToAction("Index", "Category");
		}
	}
}

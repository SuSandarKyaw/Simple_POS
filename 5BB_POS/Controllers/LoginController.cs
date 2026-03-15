using System.Security.Claims;
using _5BB_POS.Models;
using _5BB_POS.RequestModels;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _5BB_POS.Controllers;

public class LoginController : Controller
{
	private readonly SimplePosContext _context;

	public LoginController(SimplePosContext context)
	{
		_context = context;
	}

	public IActionResult Index()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> IndexAsync(LoginRequestModel request)
	{
		var user = await _context.TblUsers.FirstOrDefaultAsync(x => x.Email == request.Email);

		if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password,user.Password))
		{
			TempData["ErrorMessage"] = "Invalid Email or Password";
			return View();
		}
		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, user.FullName),
			new Claim(ClaimTypes.Email, user.Email),
			new Claim(ClaimTypes.Role, user.Role),
		};
		var claimsIdentity = new ClaimsIdentity(
		   claims, CookieAuthenticationDefaults.AuthenticationScheme);

		var authProperties = new AuthenticationProperties
		{

			IsPersistent = request.RememberMe,
			ExpiresUtc = request.RememberMe == true ? DateTimeOffset.UtcNow.AddDays(7) : DateTimeOffset.UtcNow.AddHours(8),

		};

		await HttpContext.SignInAsync(
			CookieAuthenticationDefaults.AuthenticationScheme,
			new ClaimsPrincipal(claimsIdentity),
			authProperties);
		return Redirect("/Category");
	}
}

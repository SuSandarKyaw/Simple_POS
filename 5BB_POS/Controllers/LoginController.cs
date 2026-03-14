using System.Security.Claims;
using _5BB_POS.RequestModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace _5BB_POS.Controllers;

public class LoginController : Controller
{
	private static List<UserInfo> userInfo = new List<UserInfo>()
	{
		new UserInfo
		{
			UserId = Guid.NewGuid().ToString(),
		FullName = "Admin",
		Email = "admin@gmail.com",
		Password = "admin@123",
		Role = "Admin"
		},
		new UserInfo
				{
					UserId = Guid.NewGuid().ToString(),
				FullName = "Cashier",
				Email = "cashier@gmail.com",
				Password = "cashier@123",
				Role = "Cashier"
		}
	};
	public IActionResult Index()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> IndexAsync(LoginRequestModel request)
	{
		var user =userInfo.FirstOrDefault(x=> x.Email == request.Email && x.Password == request.Password);
		if (user == null)
		{
			TempData["ErrorMessage"] = "Failed to Login";
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


public class UserInfo
{
	public string UserId { get; set; }
	public string FullName { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public string Role { get; set; }
}


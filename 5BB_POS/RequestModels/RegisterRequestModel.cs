using System.ComponentModel.DataAnnotations;

namespace _5BB_POS.RequestModels
{
	public class RegisterRequestModel
	{
		[Required(ErrorMessage = "Full Name is required")]
		[StringLength(150, ErrorMessage = "Name cannot exceed 150 characters")]
		public string FullName { get; set; }

		[Required(ErrorMessage = "Email address is required")]
		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		[RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email format")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long")]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
			ErrorMessage = "Password must have at least one uppercase, one lowercase, one number, and one special character")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Please select a role")]
		public string Role { get; set; }
	}
}

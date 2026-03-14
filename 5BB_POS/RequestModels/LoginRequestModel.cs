namespace _5BB_POS.RequestModels
{
	public class LoginRequestModel
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public bool RememberMe { get; set; }
	}
}

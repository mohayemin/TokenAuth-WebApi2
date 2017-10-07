namespace Api.Services
{
	public class ResetPasswordRequest
	{
		public string UserId { get; set; }
		public string Username { get; set; }
		public string NewPassword { get; set; }
	}
}

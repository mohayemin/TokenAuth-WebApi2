namespace Api.Services
{
	public class ResetPasswordRequest : UserIdentifier
	{
		public string NewPassword { get; set; }
	}
}

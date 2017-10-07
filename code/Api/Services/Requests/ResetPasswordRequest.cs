namespace Api.Services.Requests
{
	public class ResetPasswordRequest : UserIdentifier
	{
		public string NewPassword { get; set; }
	}
}

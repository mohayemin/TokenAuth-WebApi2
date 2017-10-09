using Microsoft.AspNetCore.Identity;

namespace Api.Services.Requests
{
	public class ChangeEmailRequest: UserIdentifier
	{
		public string NewEmail { get; set; }
	}
}
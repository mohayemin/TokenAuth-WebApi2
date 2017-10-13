using Microsoft.AspNetCore.Identity;

namespace Api.Services.Requests
{
	public class UserUpdateRequest : UserIdentifier
	{
		public string PhoneNumber { get; set; }

		public void PopulateChanges(IdentityUser user)
		{
			user.PhoneNumber = PhoneNumber;
		}
	}
}
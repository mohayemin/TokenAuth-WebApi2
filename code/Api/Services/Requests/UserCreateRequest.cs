using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Api.Services.Requests
{
    public class UserCreateRequest
    {
		[Required]
		public string Username { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }

		public IdentityUser ToDbObject()
		{
			// todo: replace with automapper
			return new IdentityUser
			{
				UserName = Username,
				Email = Email
			};
		}
    }
}

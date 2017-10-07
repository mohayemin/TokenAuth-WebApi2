using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Api.Services
{
	public class UserService
	{
		private readonly UserManager<IdentityUser> _userManager;

		public UserService(UserManager<IdentityUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<IdentityResult> Create(UserCreateRequest request)
		{
			var user = request.ToDbObject();
			var result = await _userManager.CreateAsync(user, request.Password);
			return result;
		}

		public async Task<IdentityResult> ResetPassword(ResetPasswordRequest request)
		{
			IdentityUser user;
			if (!string.IsNullOrWhiteSpace(request.UserId))
			{
				user = await _userManager.FindByIdAsync(request.UserId);
			}
			else
			{
				user = await _userManager.FindByNameAsync(request.Username);
			}

			IdentityResult result;
			if (user != null)
			{
				var token = await _userManager.GeneratePasswordResetTokenAsync(user);
				result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
			}
			else
			{
				result = IdentityResult.Failed(new IdentityError
				{
					Code = "NoSuchUser",
					Description = "Could not find user"
				});
			}

			return result;
		}
	}
}

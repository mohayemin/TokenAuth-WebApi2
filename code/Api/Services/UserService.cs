using Api.Notifications;
using Api.Services.Requests;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Api.Services
{
	public class UserService
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly INotifier _notifier;

		public UserService(UserManager<IdentityUser> userManager, INotifier notifier)
		{
			_userManager = userManager;
			_notifier = notifier;
		}

		public Task<IdentityResult> Create(UserCreateRequest request)
		{
			var user = request.ToDbObject();
			return _userManager.CreateAsync(user, request.Password);
		}

		public async Task<IdentityResult> ResetPassword(ResetPasswordRequest request)
		{
			var user = await FindUser(request);
			if (user != null)
			{
				var token = await _userManager.GeneratePasswordResetTokenAsync(user);
				var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
				
				return result;
			}

			return NoSuchUserResult();
		}

		public async Task<IdentityResult> ChangeEmail(ChangeEmailRequest request)
		{
			var user = await FindUser(request);
			if (user != null)
			{
				var oldEmail = user.Email;
				var token = await _userManager.GenerateChangeEmailTokenAsync(user, request.NewEmail);
				var result = await _userManager.ChangeEmailAsync(user, request.NewEmail, token);

				if (result.Succeeded)
					_notifier.SendNotificationAsync(user, new ChangeEmailNotification(oldEmail, request.NewEmail));

				return result;
			}

			return NoSuchUserResult();
		}

		public async Task<IdentityResult> Delete(IUserIdentifier userIdentifier)
		{
			var user = await FindUser(userIdentifier);
			if (user != null)
			{
				return await _userManager.DeleteAsync(user);
			}
			return NoSuchUserResult();
		}

		private IdentityResult NoSuchUserResult()
		{
			return IdentityResult.Failed(new IdentityError
			{
				Code = "NoSuchUser",
				Description = "Could not find user"
			});
		}

		private Task<IdentityUser> FindUser(IUserIdentifier userIdentifier)
		{
			switch (userIdentifier)
			{
				case var uid when uid.UserId != null:
					return _userManager.FindByIdAsync(userIdentifier.UserId);
				case var uid when uid.Username != null:
					return _userManager.FindByNameAsync(userIdentifier.Username);
				case var uid when uid.Email != null:
					return _userManager.FindByEmailAsync(userIdentifier.Email);
				default:
					return null;
			}
		}
	}
}

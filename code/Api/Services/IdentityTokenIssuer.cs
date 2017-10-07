
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Api.Services
{

	public class IdentityTokenIssuer : ITokenIssuer
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly ITokenBuilder _tokenBuilder;

		public IdentityTokenIssuer(UserManager<IdentityUser> userManager,
			SignInManager<IdentityUser> signInManager,
			ITokenBuilder tokenBuilder)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenBuilder = tokenBuilder;
		}

		public bool TryIssue(Credential credential, out object response)
		{
			var user = _userManager.FindByNameAsync(credential.Username).Result;

			if (user != null)
			{
				var signInResult = _signInManager.CheckPasswordSignInAsync(user, credential.Password, false).Result;

				if (signInResult.Succeeded)
				{
					response = new
					{
						user = user,
						accessToken = _tokenBuilder.Build(credential.Username)
					};
					return true;
				}
			}

			response = null;
			return false;
		}
	}
}

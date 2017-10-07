using Api.Db;
using Api.Services.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Api.Services
{
	public class IdentityTokenIssuer : ITokenIssuer
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly ITokenBuilder _tokenBuilder;
		private readonly IdentityDbContext _db;

		public IdentityTokenIssuer(UserManager<IdentityUser> userManager,
			SignInManager<IdentityUser> signInManager,
			IdentityDbContext db,
			ITokenBuilder tokenBuilder)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenBuilder = tokenBuilder;
			_db = db;
		}

		// todo: refactor long method
		public bool TryIssue(TokenIssueRequest request, out object response)
		{
			var user = _userManager.FindByNameAsync(request.Username).Result;

			if (user != null)
			{
				var signInResult = _signInManager.CheckPasswordSignInAsync(user, request.Password, false).Result;

				if (signInResult.Succeeded)
				{
					var token = _tokenBuilder.Build(user.Id);
					var refreshToken = token.RefreshToken;

					_db.Set<RefreshToken>().Attach(refreshToken);
					if (_db.Set<RefreshToken>().Any(rt => rt.UserId == user.Id))
					{
						_db.Entry(refreshToken).State = EntityState.Modified;
					}
					else
					{
						_db.Entry(refreshToken).State = EntityState.Added;
					}
					_db.SaveChangesAsync();

					response = new
					{
						userId = user.Id,
						userName = user.UserName,
						accessToken = token.AccessToken,
						refreshToken = refreshToken.Token
					};
					return true;
				}
			}

			response = null;
			return false;
		}
	}
}

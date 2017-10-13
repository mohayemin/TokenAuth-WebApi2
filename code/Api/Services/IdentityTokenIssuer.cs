using Api.Db;
using Api.Services.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

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

		public async Task<Token> Issue(TokenIssueRequest request)
		{
			var user = await FindUserAsync(request);

			if (user != null)
			{
				var roles = await _userManager.GetRolesAsync(user);
				var token = _tokenBuilder.Build(user, roles);

				await UpsertRefreshTokenAsync(token);

				return token;
			}
			return null;
		}

		private async Task UpsertRefreshTokenAsync(Token token)
		{
			var dbSet = _db.Set<RefreshToken>();
			var refreshToken = await dbSet.FindAsync(token.UserId);

			if (refreshToken != null)
			{
				refreshToken.Token = token.RefreshToken;
				refreshToken.Expires = token.RefreshTokenExpires;
				dbSet.Update(refreshToken);
			}
			else
			{
				refreshToken = new RefreshToken(token.UserId, token.RefreshToken, token.RefreshTokenExpires);
				dbSet.Add(refreshToken);
			}

			await _db.SaveChangesAsync();
		}

		private async Task<IdentityUser> FindUserAsync(TokenIssueRequest request)
		{
			var user = await FindWithRefreshToken(request.RefreshToken);

			if (user == null && request.Username != null)
			{
				user = await FindWithUsernamePassword(request.Username, request.Password);
			}

			return user;
		}

		private async Task<IdentityUser> FindWithUsernamePassword(string username, string password)
		{
			var user = await _userManager.FindByNameAsync(username);

			if (user != null)
			{
				var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);
				if (signInResult.Succeeded)
				{
					return user;
				}
			}

			return null;
		}

		private async Task<IdentityUser> FindWithRefreshToken(string refreshToken)
		{
			var dbToken = await _db.Set<RefreshToken>().FirstOrDefaultAsync(rt => rt.Token == refreshToken);

			if (dbToken != null && dbToken.Expires > DateTime.UtcNow)
			{
				return await _userManager.FindByIdAsync(dbToken.UserId);
			}
			else
			{
				return null;
			}
		}
	}
}

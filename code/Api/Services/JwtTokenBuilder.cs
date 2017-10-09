using Api.Db;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Api.Services
{

	public class JwtTokenBuilder : ITokenBuilder
	{
		private readonly ITokenConfig _config;

		public JwtTokenBuilder(ITokenConfig config)
		{
			_config = config;
		}

		public Token Build(IdentityUser user, IEnumerable<string> roles)
		{
			return new Token(user.Id,
				BuildAccessToken(user, roles),
				CryptoRandomGenerator.GenerateString(_config.RefreshTokenLength),
				DateTime.UtcNow.AddHours(_config.RefreshTokenLifetimeHours)
			);
		}

		protected string BuildAccessToken(IdentityUser user, IEnumerable<string> roles)
		{
			var claims = new List<Claim>
			{
			  new Claim(JwtRegisteredClaimNames.Sub, user.Id),
			  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

			var identity = new ClaimsIdentity(claims);

			var jwtHandler = new JwtSecurityTokenHandler();
			return jwtHandler.CreateEncodedJwt(_config.Issuer,
				_config.Audiance,
				identity,
				null,
				DateTime.UtcNow.AddMinutes(_config.AccessTokenLifeTimeMinutes),
				DateTime.UtcNow, _config.SigningCredentials);
		}
	}
}

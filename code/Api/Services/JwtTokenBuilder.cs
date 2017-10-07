using Api.Db;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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

		public Token Build(string userId)
		{
			return new Token(BuildAccessToken(userId), BuildRefreshToken(userId));
		}

		private string BuildAccessToken(string userId)
		{
			var claims = new List<Claim>
			{
			  new Claim(JwtRegisteredClaimNames.Sub, userId),
			  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			var identity = new ClaimsIdentity(claims);

			var jwtHandler = new JwtSecurityTokenHandler();
			var accessToken = jwtHandler.CreateEncodedJwt(_config.Issuer,
				_config.Audiance,
				identity,
				null,
				DateTime.UtcNow.AddMinutes(_config.AccessTokenLifeTimeMinutes),
				DateTime.UtcNow, _config.SigningCredentials);

			return accessToken;
		}

		private RefreshToken BuildRefreshToken(string userId)
		{
			var token = CryptoRandomGenerator.GenerateString(_config.RefreshTokenLength);
			return new RefreshToken(userId, token, DateTime.UtcNow.AddHours(_config.RefreshTokenLifetimeHours));
		}
	}
}

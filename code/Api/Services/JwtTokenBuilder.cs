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
			return new Token(userId,
				BuildAccessToken(userId),
				CryptoRandomGenerator.GenerateString(_config.RefreshTokenLength),
				DateTime.UtcNow.AddHours(_config.RefreshTokenLifetimeHours)
			);
		}

		protected string BuildAccessToken(string userId)
		{
			var claims = new List<Claim>
			{
			  new Claim(JwtRegisteredClaimNames.Sub, userId),
			  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

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

using Microsoft.IdentityModel.Tokens;
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

		public string Build(string username)
		{
			var claims = new List<Claim>
			{
			  new Claim(JwtRegisteredClaimNames.Sub, username),
			  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			var identity = new ClaimsIdentity(claims);

			var jwtHandler = new JwtSecurityTokenHandler();
			var token = jwtHandler.CreateEncodedJwt(_config.Issuer,
				_config.Audiance,
				identity,
				null,
				DateTime.UtcNow.AddMinutes(_config.LifeTimeMinutes),
				DateTime.UtcNow, _config.SigningCredentials);

			return token;
		}
	}
}

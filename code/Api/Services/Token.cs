using System;

namespace Api.Services
{
	public class Token
	{
		public Token(string userId, string accessToken, string refreshToken, DateTime refreshTokenExpires)
		{
			UserId = userId;
			AccessToken = accessToken;
			RefreshToken = refreshToken;
			RefreshTokenExpires = refreshTokenExpires;
		}

		public string UserId { get; set; }
		public string AccessToken { get; }
		public string RefreshToken { get; }
		public DateTime RefreshTokenExpires { get; }
	}
}

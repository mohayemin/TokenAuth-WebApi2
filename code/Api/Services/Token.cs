using Api.Db;

namespace Api.Services
{
	public class Token
	{
		public Token(string accessToken, RefreshToken refreshToken)
		{
			AccessToken = accessToken;
			RefreshToken = refreshToken;
		}

		public string AccessToken { get; }
		public RefreshToken RefreshToken { get; }
	}
}

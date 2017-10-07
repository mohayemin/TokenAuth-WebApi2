using Microsoft.IdentityModel.Tokens;

namespace Api.Services
{
	public interface ITokenConfig
	{
		string Issuer { get; }
		string Audiance { get; }
		double AccessTokenLifeTimeMinutes { get; }

		int RefreshTokenLength { get; }
		double RefreshTokenLifetimeHours { get; }

		SigningCredentials SigningCredentials { get; }
	}
}
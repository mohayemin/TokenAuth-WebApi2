using Microsoft.IdentityModel.Tokens;

namespace Api.Services
{
	public interface ITokenConfig
	{
		string Issuer { get; }
		string Audiance { get; }
		double LifeTimeMinutes { get; }

		SigningCredentials SigningCredentials { get; }
	}
}
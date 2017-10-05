using System.IdentityModel.Tokens.Jwt;

namespace Api.Services
{
	public interface ITokenBuilder
    {
		string Build(string username);
    }
}

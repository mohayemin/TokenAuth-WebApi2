using Api.Services.Requests;

namespace Api.Services
{
	public interface ITokenIssuer
	{
		bool TryIssue(Credential credential, out object response);
	}
}

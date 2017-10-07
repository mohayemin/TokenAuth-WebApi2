using Api.Services.Requests;

namespace Api.Services
{
	public interface ITokenIssuer
	{
		bool TryIssue(TokenIssueRequest request, out object response);
	}
}

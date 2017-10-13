using Api.Services.Requests;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Services
{
	public interface ITokenIssuer
	{
		Task<Token> Issue(TokenIssueRequest request);
		Task<bool> RevokeAsync(ClaimsPrincipal principal);
	}
}

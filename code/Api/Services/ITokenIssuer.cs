using Api.Services.Requests;
using System.Threading.Tasks;

namespace Api.Services
{
	public interface ITokenIssuer
	{
		Task<Token> Issue(TokenIssueRequest request);
	}
}

using System.Threading.Tasks;
using Api.Services.Requests;
using System;

namespace Api.Services.Samples
{

	public class SampleTokenIssuer : ITokenIssuer
	{
		private readonly ITokenBuilder _tokenBuilder;

		public SampleTokenIssuer(ITokenBuilder tokenBuilder)
		{
			_tokenBuilder = tokenBuilder;
		}

		public Task<Token> Issue(TokenIssueRequest request)
		{
			if (request.Username != null && request.Username == request.Password)
			{
				var userId = $"id_{request.Username}";
				return Task.FromResult(_tokenBuilder.Build(userId));
			}
			return null;
		}
	}
}

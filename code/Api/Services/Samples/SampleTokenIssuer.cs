
namespace Api.Services.Samples
{

	public class SampleTokenIssuer : ITokenIssuer
	{
		private readonly ITokenBuilder _tokenBuilder;

		public SampleTokenIssuer(ITokenBuilder tokenBuilder)
		{
			_tokenBuilder = tokenBuilder;
		}

		public bool TryIssue(Credential credential, out object response)
		{
			if (credential.Username != null && credential.Username == credential.Password)
			{
				response = new {
					Username = credential.Username,
					Token = _tokenBuilder.Build(credential.Username)
				};

				return true;
			}

			response = null;
			return false;
		}
	}
}

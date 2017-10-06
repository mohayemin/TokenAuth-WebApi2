using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace TokeAuth.Controllers
{
	[Route("token")]
	public class TokenController : Controller
	{
		private readonly ITokenIssuer _issuer;

		public TokenController(ITokenIssuer issuer)
		{
			_issuer = issuer;
		}

		[HttpPost]
		public IActionResult Issue([FromBody]Credential credential)
		{
			if (_issuer.TryIssue(credential, out object response))
			{
				return Ok(response);
			}
			return Unauthorized();
		}
	}
}

using Api.Services;
using Api.Services.Requests;
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
		public IActionResult Issue([FromBody]TokenIssueRequest request)
		{
			if (_issuer.TryIssue(request, out object response))
			{
				return Ok(response);
			}
			return Unauthorized();
		}
	}
}

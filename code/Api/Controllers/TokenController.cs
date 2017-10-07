using Api.Services;
using Api.Services.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
		public async Task<IActionResult> Issue([FromBody]TokenIssueRequest request)
		{
			var token = await _issuer.Issue(request);
			if (token != null)
			{
				return Ok(token);
			}
			return Unauthorized();
		}
	}
}

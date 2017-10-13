using Api.Services;
using Api.Services.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

		/// <summary>
		/// API for issuing a access token.
		/// </summary>
		/// /// <remarks>
		/// Sample request:
		///     {
		///        "Username": "admin",
		///        "Password": "123"
		///     }
		///
		/// </remarks>
		/// <param name="request">
		/// Either a RefreshToken or Username and Password
		/// </param>
		/// <returns>
		/// A token object (<see cref="Token"/>)
		/// </returns>
		/// <response code="200">Issue successful</response>
		/// <response code="400">Issue failed</response>           
		[HttpPost(nameof(Issue))]
		public async Task<IActionResult> Issue([FromBody]TokenIssueRequest request)
		{
			var token = await _issuer.Issue(request);
			if (token != null)
			{
				return Ok(token);
			}
			return StatusCode(401);
		}

		/// <summary>
		/// Revokes refresh token
		/// </summary>
		/// <returns></returns>
		[HttpGet(nameof(Revoke))]
		[Authorize]
		public async Task<IActionResult> Revoke()
		{
			var revoked = await _issuer.RevokeAsync(User);
			return Ok(revoked);
		}
	}
}

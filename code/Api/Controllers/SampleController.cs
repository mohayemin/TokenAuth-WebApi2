using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[Route("sample")]
	public class SampleController : Controller
    {
		/// <summary>
		/// Sample API that does not require any token
		/// </summary>
		/// <returns></returns>
		[HttpGet("freeforall")]
		public IActionResult FreeForAll()
		{
			return Ok("accessible without access token");
		}

		/// <summary>
		/// Sample API that requires a token, but no specific role 
		/// </summary>
		/// <returns></returns>
		/// <response code="200">Valid access token passed</response>
		/// <response code="401">Access token not passed or passed but invalid</response>   
		[HttpGet("requirestoken")]
		[Authorize]
		public IActionResult RequiresToken()
		{
			return Ok();
		}

		/// <summary>
		/// Sample API that requires "admin" role
		/// </summary>
		/// <returns></returns>
		/// <response code="200">Access token passed and user has admin role</response>
		/// <response code="401">Access token not passed or passed but invalid or passed valid but used does not have 'admin' role</response>   
		[HttpGet("requiresadmin")]
		[Authorize(Roles = BuiltInRoles.Admin)]
		public IActionResult RequiresAdmin()
		{
			return Ok("accessible only with access token");
		}
	}
}

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
		[HttpGet("requirestoken")]
		[Authorize]
		public IActionResult RequiresToken()
		{
			return Ok("accessible only with access token");
		}

		/// <summary>
		/// Sample API that requires "admin" role
		/// </summary>
		/// <returns></returns>
		[HttpGet("requiresadmin")]
		[Authorize(Roles = BuiltInRoles.Admin)]
		public IActionResult RequiresAdmin()
		{
			return Ok("accessible only with access token");
		}
	}
}

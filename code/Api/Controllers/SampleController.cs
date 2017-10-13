using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[Route("sample")]
	public class SampleController : Controller
    {
		/// <summary>
		/// This action is does not require any token
		/// </summary>
		/// <returns></returns>
		[HttpGet("freeforall")]
		public IActionResult FreeForAll()
		{
			return Ok("accessible without access token");
		}

		[HttpGet("requirestoken")]
		[Authorize]
		public IActionResult RequiresToken()
		{
			return Ok("accessible only with access token");
		}

		[HttpGet("requiresadmin")]
		[Authorize(Roles = BuiltInRoles.Admin)]
		public IActionResult RequiresAdmin()
		{
			return Ok("accessible only with access token");
		}
	}
}

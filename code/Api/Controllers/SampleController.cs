using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[Route("sample")]
	public class SampleController : Controller
    {
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
	}
}

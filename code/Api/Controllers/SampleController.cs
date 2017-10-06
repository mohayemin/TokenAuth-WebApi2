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
    }
}

using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace TokeAuth.Controllers
{
	[Route("token")]
	public class TokenController : Controller
	{
		private readonly ITokenBuilder _builder;

		public TokenController(ITokenBuilder builder)
		{
			_builder = builder;
		}

		[HttpPost]
		public IActionResult Issue([FromBody]string username, [FromBody]string password)
		{
			if (username != null && username != password)
			{
				return Unauthorized();
			}
			else
			{
				var token = _builder.Build("mohayemin");
				return Ok(token);
			}
		}
	}
}

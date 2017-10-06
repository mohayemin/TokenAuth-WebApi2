using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
	[Route("user")]
	public class UserController : Controller
	{
		private readonly UserManager<IdentityUser> userManager;

		public UserController(UserManager<IdentityUser> userManager)
		{
			this.userManager = userManager;
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody]string username)
		{
			username = "moha";
			var user = new IdentityUser() { UserName = username };
			var result = await userManager.CreateAsync(user, "123");

			if (result.Succeeded)
			{
				return Created($"user/{username}", user);
			}
			return StatusCode(500, result.Errors);
		}
	}
}

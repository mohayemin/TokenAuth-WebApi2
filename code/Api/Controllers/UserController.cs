using Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
	[Route("user")]
	public class UserController : Controller
	{
		private readonly UserManager<IdentityUser> _userManager;

		public UserController(UserManager<IdentityUser> userManager)
		{
			_userManager = userManager;
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody]UserCreateRequest request)
		{
			var user = request.ToDbObject();
			var result = await _userManager.CreateAsync(user, request.Password);

			if (result.Succeeded)
			{
				return Created($"user/{user.Id}", user);
			}
			return StatusCode(500, result.Errors);
		}
	}
}

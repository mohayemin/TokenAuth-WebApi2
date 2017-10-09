using Api.Services;
using Api.Services.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
	//todo: [Authorize(Roles = "admin")]
	[Route("user")]
	public class UserController : Controller
	{
		private readonly UserService _service;

		public UserController(UserService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> Get(UserIdentifier identifier)
		{
			var user = await _service.Find(identifier);
			return (user == null) ? (IActionResult)NotFound() : Ok(user);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody]UserCreateRequest request)
		{
			var result = await _service.Create(request);
			return GetResult(result, 201);
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody]UserUpdateRequest request)
		{
			var result = await _service.UpdateAsync(request);			
			return GetResult(result, 204);
		}

		[HttpPost(nameof(ResetPassword))]
		public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordRequest request)
		{
			var result = await _service.ResetPassword(request);
			return GetResult(result, 204);
		}

		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody]UserIdentifier identifier)
		{
			var result = await _service.Delete(identifier);
			return GetResult(result, 204);
		}

		[HttpPut(nameof(ChangeEmail))]
		public async Task<IActionResult> ChangeEmail([FromBody]ChangeEmailRequest request)
		{
			var result = await _service.ChangeEmail(request);
			return GetResult(result, 204);
		}

		[HttpPut(nameof(AddRoles))]
		public async Task<IActionResult> AddRoles([FromBody]RoleAssignmentRequest request)
		{
			var result = await _service.AddRolesAsync(request);
			return GetResult(result, 204);
		}

		private IActionResult GetResult(IdentityResult result, int successCode)
		{
			switch (result)
			{
				case var r when r.Succeeded:
					return StatusCode(successCode);
				case var r when r.Errors.Any(e => e.Code == "NoSuchUser"):
					return StatusCode(404, result.Errors);
				default:
					return StatusCode(500, result.Errors);
			}
		}
	}
}

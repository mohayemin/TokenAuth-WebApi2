using Api.Services;
using Api.Services.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
	/// <summary>
	/// API for admin operations on a user.
	/// <para>
	/// Note: All the request objects in the API in this controller extends <see cref="UserIdentifier"/>.
	/// </para>
	/// </summary>
	[Route("user")]
	[Authorize(Roles = BuiltInRoles.Admin)]
	public class UserController : Controller
	{
		private readonly UserService _service;

		public UserController(UserService service)
		{
			_service = service;
		}

		/// <summary>
		/// Get one single user
		/// </summary>
		/// <param name="identifier"></param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> Get(UserIdentifier identifier)
		{
			var user = await _service.Find(identifier);
			return (user == null) ? (IActionResult)NotFound() : Ok(user);
		}

		/// <summary>
		/// Create a user
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Create([FromBody]UserCreateRequest request)
		{
			var result = await _service.Create(request);
			return GetResult(result, 201);
		}

		/// <summary>
		/// Update a user
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut]
		public async Task<IActionResult> Update([FromBody]UserUpdateRequest request)
		{
			var result = await _service.UpdateAsync(request);
			return GetResult(result, 204);
		}

		/// <summary>
		/// Reset password of a user
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut(nameof(ResetPassword))]
		public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordRequest request)
		{
			var result = await _service.ResetPassword(request);
			return GetResult(result, 204);
		}

		/// <summary>
		/// Delete a user
		/// </summary>
		/// <param name="identifier"></param>
		/// <returns></returns>
		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody]UserIdentifier identifier)
		{
			var result = await _service.Delete(identifier);
			return GetResult(result, 204);
		}

		/// <summary>
		/// Change email address of a user
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut(nameof(ChangeEmail))]
		public async Task<IActionResult> ChangeEmail([FromBody]ChangeEmailRequest request)
		{
			var result = await _service.ChangeEmail(request);
			return GetResult(result, 204);
		}

		/// <summary>
		/// Add roels to a user
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut(nameof(AddRoles))]
		public async Task<IActionResult> AddRoles([FromBody]RoleAssignmentRequest request)
		{
			var result = await _service.AddRolesAsync(request);
			return GetResult(result, 204);
		}

		/// <summary>
		/// Deletes roles from a user
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut(nameof(DeleteRoles))]
		public async Task<IActionResult> DeleteRoles([FromBody]RoleAssignmentRequest request)
		{
			var result = await _service.DeleteRolesAsync(request);
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

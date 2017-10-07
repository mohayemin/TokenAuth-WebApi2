using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
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

		[HttpPost]
		public async Task<IActionResult> Create([FromBody]UserCreateRequest request)
		{
			var result = await _service.Create(request);
			return GetResult(result, 201);
		}

		[HttpPost(nameof(ResetPassword))]
		public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordRequest request)
		{
			var result = await _service.ResetPassword(request);
			return GetResult(result, 200);
		}

		private IActionResult GetResult(IdentityResult result, int successCode)
		{
			if (result.Succeeded)
			{
				return StatusCode(successCode);
			}
			return StatusCode(500, result.Errors);
		}
	}
}


using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Api.Services
{
	public interface ITokenBuilder
    {
		Token Build(IdentityUser user, IEnumerable<string> roles);
    }
}

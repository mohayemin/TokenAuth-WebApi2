using System.Collections.Generic;

namespace Api.Services.Requests
{

	public class RoleAssignmentRequest : UserIdentifier
	{
		public IEnumerable<string> Roles { get; set; }
	}
}
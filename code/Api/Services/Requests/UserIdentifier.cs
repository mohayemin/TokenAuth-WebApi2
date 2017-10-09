namespace Api.Services.Requests
{
	public class UserIdentifier: IUserIdentifier
	{
		public string Id { get; set;  }
		public string Username { get; set;  }
		public string Email { get; set; }
	}
}
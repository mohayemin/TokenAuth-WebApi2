namespace Api.Services.Requests
{

	public class UserIdentifier: IUserIdentifier
	{
		public string UserId { get; set;  }
		public string Username { get; set;  }
	}
}
namespace Api.Services.Requests
{
	/// <summary>
	/// Model to uniquely identify a user. This class has three properties. 
	/// Exactly one of the properties of this class is requried to identify a user.
	/// </summary>
	public class UserIdentifier : IUserIdentifier
	{
		public string Id { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
	}
}
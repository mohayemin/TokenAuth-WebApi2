namespace Api.Services.Requests
{
	public interface IUserIdentifier
	{
		string Id { get; }
		string Username { get; }
		string Email { get; }
	}
}
namespace Api.Services.Requests
{
	public class TokenIssueRequest
    {
		public string Username { get; set; }
		public string Password { get; set; }
		public string RefreshToken { get; set; }
	}
}

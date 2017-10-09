using Api.Notifications;

namespace Api.Services
{
	public class ChangeEmailNotification : INotification
	{
		private readonly string _oldEmail;
		private readonly string _newEmail;

		public ChangeEmailNotification(string oldEmail, string newEmail)
		{
			_oldEmail = oldEmail;
			_newEmail = newEmail;
		}

		public string Subject => "Your email has been changed";

		public string Message => $"Your email has been changed from {_oldEmail} to {_newEmail}";
	}
}

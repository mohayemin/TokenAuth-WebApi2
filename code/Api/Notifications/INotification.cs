namespace Api.Notifications
{
    public interface INotification
    {
		string Subject { get; }
		string Message { get; }
    }
}

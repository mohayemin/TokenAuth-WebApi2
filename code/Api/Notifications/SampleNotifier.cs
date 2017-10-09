using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System;

namespace Api.Notifications
{
	public class SampleNotifier : INotifier
	{
		public async Task SendNotificationAsync(IdentityUser to, INotification notification)
		{
			Console.WriteLine($"sending notification to {to.UserName}");
			Console.WriteLine($"Subject: {notification.Subject}");
			Console.WriteLine($"Message: {notification.Message}");

			await Task.CompletedTask;
		}
	}
}

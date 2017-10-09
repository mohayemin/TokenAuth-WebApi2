using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Api.Notifications
{
	public interface INotifier
	{
		Task SendNotificationAsync(IdentityUser to, INotification notification);
	}
}

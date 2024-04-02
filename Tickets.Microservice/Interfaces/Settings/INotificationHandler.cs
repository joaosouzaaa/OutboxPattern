using Tickets.Microservice.Settings.NotificationSettings;

namespace Tickets.Microservice.Interfaces.Settings;

public interface INotificationHandler
{
    List<Notification> GetNotifications();
    bool HasNotifications();
    void AddNotification(string key, string message);
}

using Support.Microservice.Settings.NotificationSettings;

namespace Support.Microservice.Interfaces.Settings;

public interface INotificationHandler
{
    List<Notification> GetNotifications();
    bool HasNotifications();
    void AddNotification(string key, string message);
}

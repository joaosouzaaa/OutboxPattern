using Support.Microservice.Settings.NotificationSettings;

namespace Support.Microservice.Interfaces.Setttings;

public interface INotificationHandler
{
    List<Notification> GetNotifications();
    bool HasNotifications();
    void AddNotification(string key, string message);
}

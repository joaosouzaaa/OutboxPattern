﻿using Tickets.Microservice.Interfaces.Settings;

namespace Tickets.Microservice.Settings.NotificationSettings;

public sealed class NotificationHandler : INotificationHandler
{
    private readonly List<Notification> _notificationList;

    public NotificationHandler()
    {
        _notificationList = [];
    }

    public List<Notification> GetNotifications() =>
        _notificationList;

    public bool HasNotifications() =>
        _notificationList.Any();

    public void AddNotification(string key, string message) =>
        _notificationList.Add(new Notification()
        {
            Key = key,
            Message = message
        });
}

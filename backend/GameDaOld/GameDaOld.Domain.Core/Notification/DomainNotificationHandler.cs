namespace GameDaOld.Domain.Core;

public class DomainNotificationHandler: IDomainNotificationHandler
{
    private readonly List<DomainNotificationEvent> _notifications;

    public DomainNotificationHandler()
    {
        _notifications = new List<DomainNotificationEvent>();
    }

    public void AddNotification(string message, eNotificationStatusEvent status)
    {
        _notifications.Add(new DomainNotificationEvent(message, status));
    }

    public void NotifyError(string message)
    {
        AddNotification(message, eNotificationStatusEvent.Error);
    }

    public void NotifySuccess(string message)
    {
        AddNotification(message, eNotificationStatusEvent.Success);
    }

    public void NotifyWarning(string message)
    {
        AddNotification(message, eNotificationStatusEvent.Warning);
    }

    public IReadOnlyList<DomainNotificationEvent> GetNotifications() => _notifications.AsReadOnly();

    public void Clear() => _notifications.Clear();

    public IEnumerable<DomainNotificationEvent> GetNotificationsError() =>
        _notifications.Where(x => x.Status == eNotificationStatusEvent.Error);

    public IEnumerable<DomainNotificationEvent> GetNotificationsSuccess() =>
        _notifications.Where(x => x.Status == eNotificationStatusEvent.Success);

    public bool HasNotifications() => _notifications.Count > 0;
}

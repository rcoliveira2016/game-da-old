namespace GameDaOld.Domain.Core;

public class DomainNotificationEvent
{
    public string Name { get; }
    public string Message { get; }
    public eNotificationStatusEvent Status { get; }

    public DomainNotificationEvent(string message, eNotificationStatusEvent status)
    {
        Name = nameof(DomainNotificationEvent);
        Message = message;
        Status = status;
    }
}

public enum eNotificationStatusEvent
{
    Error,
    Success,
    Warning
}
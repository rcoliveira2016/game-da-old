using System.Collections.Generic;

namespace GameDaOld.Domain.Core
{
    public interface IDomainNotificationHandler
    {
        void AddNotification(string message, eNotificationStatusEvent status);
        void NotifyError(string message);
        void NotifySuccess(string message);
        void NotifyWarning(string message);
        IReadOnlyList<DomainNotificationEvent> GetNotifications();
        IEnumerable<DomainNotificationEvent> GetNotificationsError();
        IEnumerable<DomainNotificationEvent> GetNotificationsSuccess();
        bool HasNotifications();
        void Clear();

    }
}

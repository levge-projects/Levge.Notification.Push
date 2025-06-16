using Levge.Notification.Push.Models;

namespace Levge.Notification.Push.Interfaces
{
    public interface IPushSender
    {
        Task SendAsync(PushMessage message, CancellationToken cancellationToken = default);
    }
}

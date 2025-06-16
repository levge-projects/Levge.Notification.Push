using Levge.Notification.Push.Interfaces;
using Levge.Notification.Push.Models;

namespace Levge.Notification.Push.Providers
{
    internal class FakePushSender : IPushSender
    {
        public Task SendAsync(PushMessage message, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"[FAKE PUSH] => Tokens: {string.Join(",", message.Tokens)} | Title: {message.Title}");
            return Task.CompletedTask;
        }
    }
}

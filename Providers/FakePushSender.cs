using Levge.Exceptions;
using Levge.Extensions;
using Levge.Notification.Push.Interfaces;
using Levge.Notification.Push.Models;
using Microsoft.Extensions.Logging;

namespace Levge.Notification.Push.Providers
{
    internal class FakePushSender : IPushSender
    {
        private readonly ILogger<FakePushSender> _logger;

        public FakePushSender(ILogger<FakePushSender> logger)
        {
            _logger = logger;
        }

        public Task SendAsync(PushMessage message, CancellationToken cancellationToken = default, bool fireAndForget = false)
        {
            if (fireAndForget)
            {
                SendInternalAsync(message).FireAndForget(_logger, nameof(FakePushSender));
                return Task.CompletedTask;
            }

            return SendInternalAsync(message);
        }

        private Task SendInternalAsync(PushMessage message)
        {
            try
            {
                var target = !string.IsNullOrWhiteSpace(message.Topic)
                    ? $"[Topic: {message.Topic}]"
                    : $"[Tokens: {string.Join(", ", message.Tokens)}]";

                _logger.LogInformation("[FAKE PUSH] → {Target} | Title: {Title} | Body: {Body}", target, message.Title, message.Body);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "FakePushSender.SendAsync");
                throw new LevgeException("Fake push failed.", ex);
            }
        }
    }
}

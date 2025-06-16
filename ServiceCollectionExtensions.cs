using Levge.Notification.Push.Interfaces;
using Levge.Notification.Push.Models;
using Levge.Notification.Push.Providers;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPushNotification(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PushConfig>(configuration.GetSection("PushConfig"));

            var config = configuration.GetSection("PushConfig").Get<PushConfig>();
            var provider = config.Provider?.ToLowerInvariant();

            return provider switch
            {
                "firebase" => services.AddSingleton<IPushSender, FirebasePushSender>(),
                "fake" => services.AddSingleton<IPushSender, FakePushSender>(),
                _ => throw new NotSupportedException($"Push provider '{provider}' not supported.")
            };
        }
    }
}

using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Levge.Notification.Push.Interfaces;
using Levge.Notification.Push.Models;
using Microsoft.Extensions.Options;

namespace Levge.Notification.Push.Providers
{
    internal class FirebasePushSender : IPushSender
    {
        private readonly FirebaseConfig _firebaseConfig;
        private readonly Lazy<FirebaseApp> _firebaseApp;

        public FirebasePushSender(IOptions<PushConfig> options)
        {
            _firebaseConfig = options.Value.Firebase ?? throw new InvalidOperationException("Firebase config missing.");

            _firebaseApp = new Lazy<FirebaseApp>(() =>
            {
                GoogleCredential credential;

                if (_firebaseConfig.UseJsonFile)
                {
                    if (!File.Exists(_firebaseConfig.ServiceAccountJsonPath))
                        throw new FileNotFoundException($"Service account file not found at '{_firebaseConfig.ServiceAccountJsonPath}'");

                    credential = GoogleCredential
                        .FromFile(_firebaseConfig.ServiceAccountJsonPath);
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(_firebaseConfig.ServiceAccountJson))
                        throw new InvalidOperationException("ServiceAccountJson is missing.");

                    credential = GoogleCredential
                        .FromJson(_firebaseConfig.ServiceAccountJson);
                }

                return FirebaseApp.Create(new AppOptions
                {
                    Credential = credential,
                    ProjectId = _firebaseConfig.ProjectId
                });
            });
        }

        public async Task SendAsync(PushMessage message, CancellationToken cancellationToken = default)
        {
            var messaging = FirebaseMessaging.GetMessaging(_firebaseApp.Value);

            var notification = new FirebaseAdmin.Messaging.Notification
            {
                Title = message.Title,
                Body = message.Body
            };

            var data = message.Data ?? new Dictionary<string, string>();

            if (message.Topic != null)
            {
                var msg = new Message
                {
                    Topic = message.Topic,
                    Notification = notification,
                    Data = data
                };

                await messaging.SendAsync(msg, cancellationToken);
                return;
            }

            if (message.Tokens.Count == 1)
            {
                var msg = new Message
                {
                    Token = message.Tokens.First(),
                    Notification = notification,
                    Data = data
                };

                await messaging.SendAsync(msg, cancellationToken);
            }
            else
            {
                var multicast = new MulticastMessage
                {
                    Tokens = message.Tokens,
                    Notification = notification,
                    Data = data
                };

                var response = await messaging.SendEachForMulticastAsync(multicast, cancellationToken);
            }
        }
    }
}

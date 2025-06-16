namespace Levge.Notification.Push.Models
{
    internal class PushConfig
    {
        public string Provider { get; set; } = "Firebase";
        public FirebaseConfig? Firebase { get; set; }
    }
}

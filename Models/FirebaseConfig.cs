namespace Levge.Notification.Push.Models
{
    internal class FirebaseConfig
    {
        public string ProjectId { get; set; } = null!;

        public bool UseJsonFile { get; set; } = true;
        public string ServiceAccountJsonPath { get; set; } = null!;
        public string? ServiceAccountJson { get; set; }
    }
}

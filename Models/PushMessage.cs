using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levge.Notification.Push.Models
{
    public class PushMessage
    {
        public List<string> Tokens { get; set; } = new();
        public string? Topic { get; set; }

        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;

        public Dictionary<string, string>? Data { get; set; }
    }
}

# Levge.Notification.Push

[![Publish NuGet Package](https://github.com/levge-projects/Levge.Notification.Push/actions/workflows/main.yml/badge.svg)](https://github.com/levge-projects/Levge.Notification.Push/actions/workflows/main.yml)
[![NuGet](https://img.shields.io/nuget/v/Levge.Notification.Push.svg)](https://www.nuget.org/packages/Levge.Notification.Push)

Provider-based push notification library for .NET 8, supporting Firebase and Fake senders. Extensible architecture designed for web and mobile use-cases with DI integration.

---

## 📦 Installation

```bash
dotnet add package Levge.Notification.Push
```

---

## ⚙️ Configuration

Add this to your `appsettings.json`:

```json
"PushConfig": {
  "Provider": "Firebase",
  "Firebase": {
    "ProjectId": "your-project-id",
    "UseJsonFile": true,
    "ServiceAccountJsonPath": "firebase/serviceAccount.json"
  }
}
```

---

## 🔧 Setup in `Program.cs`

```csharp
builder.Services.AddPushNotification(builder.Configuration);
```

---

## 📤 Usage

```csharp
public class PushTestService
{
    private readonly IPushSender _pushSender;

    public PushTestService(IPushSender pushSender)
    {
        _pushSender = pushSender;
    }

    public async Task SendTestAsync()
    {
        await _pushSender.SendAsync(new PushMessage
        {
            Tokens = new List<string> { "device_token_1", "device_token_2" },
            Title = "Welcome!",
            Body = "This is a push notification",
            Data = new Dictionary<string, string>
            {
                { "type", "test" }
            }
        });
    }
}
```

---

## 🧩 Providers

| Provider   | Status       |
|------------|--------------|
| `Firebase` | ✅ Supported |
| `Fake`     | ✅ Supported |
| `SignalR`  | 🔜 Planned   |

---

## 🛡️ License

MIT © [Levge](https://github.com/levge-projects)

namespace LocalNotificationXF
{
    using System;

    public interface INotificationManager
    {
        event EventHandler NotificationReceived;
        void Initialize();
        void SendNotification(string title, string subtitle, string message, DateTime? notifyTime = null);
        void ReceiveNotification(string title, string subtitle, string message);
    }
}

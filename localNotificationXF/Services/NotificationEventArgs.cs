namespace LocalNotificationXF.Services
{
    using System;

    public class NotificationEventArgs : EventArgs
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Message { get; set; }
    }
}

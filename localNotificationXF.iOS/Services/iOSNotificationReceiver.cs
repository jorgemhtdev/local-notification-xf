namespace LocalNotificationXF.iOS.Services
{
    using System;
    using UserNotifications;
    using Xamarin.Forms;

    public class iOSNotificationReceiver : UNUserNotificationCenterDelegate
    {
        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            ProcessNotification(notification);
            completionHandler(UNNotificationPresentationOptions.Alert);
        }

        void ProcessNotification(UNNotification notification)
        {
            string title = notification.Request.Content.Title;
            string subtitle = notification.Request.Content.Subtitle;
            string message = notification.Request.Content.Body;

            DependencyService.Get<INotificationManager>()
                             .ReceiveNotification(title, message, subtitle);
        }
    }
}
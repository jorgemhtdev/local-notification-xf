using System;
using Foundation;
using LocalNotificationXF.Services;
using UserNotifications;
using Xamarin.Forms;

// https://developer.apple.com/documentation/usernotifications
// https://developer.apple.com/documentation/usernotifications/scheduling_a_notification_locally_from_your_app

[assembly: Dependency(typeof(LocalNotificationXF.iOS.iOSNotificationManager))]
namespace LocalNotificationXF.iOS
{
    public class iOSNotificationManager : INotificationManager
    {
        bool hasNotificationsPermission;

        public event EventHandler NotificationReceived;

        public void Initialize() //  Solicitar Permiso
        {
            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert,  (approved, err) =>
            {
                hasNotificationsPermission = approved;
            });
        }

        public void SendNotification(string title, string subtitle, string message, DateTime? notifyTime = null)
        {
            if (!hasNotificationsPermission)
            {
                return;
            }

            // 1. Creamos el Trigger de la Notificación
            UNNotificationTrigger trigger; 
            if (notifyTime != null)
            {
                trigger = UNCalendarNotificationTrigger.CreateTrigger(GetNSDateComponents(notifyTime.Value), false);
            }
            else
            {
                trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(0.25, false);
            }

            // 2. Creamos el contenido de la Notificación
            var content = new UNMutableNotificationContent()
            {
                Title = title,
                Subtitle = subtitle,
                Body = message,
                Badge = 1
            };

            // 3. Creamos la Request
            var request = UNNotificationRequest.FromIdentifier(Guid.NewGuid().ToString(), content, trigger);

            // 4. Añadimos la Request al Centro de Notificaciones
            UNUserNotificationCenter.Current.RemoveAllPendingNotificationRequests();
            UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) =>
            {
                if (err != null)
                {
                    throw new Exception($"Some Error");
                }
            });
        }

        public void ReceiveNotification(string title, string subtitle, string message)
        {
            var args = new NotificationEventArgs()
            {
                Title = title,
                Subtitle = subtitle,
                Message = message
            };
            NotificationReceived?.Invoke(null, args);
        }

        NSDateComponents GetNSDateComponents(DateTime dateTime)
        {
            return new NSDateComponents
            {
                Month = dateTime.Month,
                Day = dateTime.Day,
                Year = dateTime.Year,
                Hour = dateTime.Hour,
                Minute = dateTime.Minute,
                Second = dateTime.Second
            };
        }
    }
}
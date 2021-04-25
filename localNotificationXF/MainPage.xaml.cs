namespace localNotificationXF
{
    using System;
    using LocalNotificationXF;
    using LocalNotificationXF.Services;
    using Xamarin.Forms;

    public partial class MainPage : ContentPage
    {
        INotificationManager notificationManager;

        public MainPage()
        {
            InitializeComponent();

            notificationManager = DependencyService.Get<INotificationManager>();
            notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                // NOTIFICACIÓN RECIBIDA => REALIZAR OPERACIÓN X
            };
        }

        void OnScheduleClick(object sender, EventArgs e)
        {
            string title = $"Notifiación Molona";
            string subtitle = string.Empty;
            string message = $"Notificación programada hace 10 seg";
            notificationManager.SendNotification(title, subtitle, message, DateTime.Now.AddSeconds(10));
        }

        void OnSendClick(object sender, EventArgs e)
        {
            string title = $"Notifiación Molona";
            string subtitle = string.Empty;
            string message = $"Recuerde sincronizar todos los informes";
            notificationManager.SendNotification(title, subtitle, message);
        }
    }
}

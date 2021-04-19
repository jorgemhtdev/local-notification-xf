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
            };
        }

        void OnSendClick(object sender, EventArgs e)
        {
            string title = $"Recordatorio";
            string subtitle = string.Empty;
            string message = $"Recuerde sincronizar todos los informes";
            notificationManager.SendNotification(title, subtitle, message);
        }

        void OnScheduleClick(object sender, EventArgs e)
        {
            string title = $"Ubicación";
            string subtitle = string.Empty;
            string message = $"Ha salido de su zona comercial";
            notificationManager.SendNotification(title, subtitle, message, DateTime.Now.AddSeconds(10));
        }
    }
}

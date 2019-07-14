using Android.App;
using Android.Content;
using Android.Graphics;

namespace Youtube.Manager.Droid
{
    public class NotificationHelper : ContextWrapper
    {
        public const string PRIMARY_CHANNEL = "default";
        public const string SECONDARY_CHANNEL = "second";
        private readonly Context _context;

        private NotificationManager manager;

        public NotificationHelper(Context context) : base(context)
        {
            _context = context;
            var chan1 = new NotificationChannel(PRIMARY_CHANNEL, GetString(Resource.String.noti_channel_default),
                NotificationImportance.Default);
            chan1.LightColor = Color.DarkRed;
            chan1.LockscreenVisibility = NotificationVisibility.Private;
            //chan1.EnableVibration(false);
            //chan1.EnableLights(false);
            //chan1.SetSound(null, null);

            Manager.CreateNotificationChannel(chan1);


            var chan2 = new NotificationChannel(SECONDARY_CHANNEL, GetString(Resource.String.noti_channel_second),
                NotificationImportance.High);
            chan2.LightColor = Color.DarkRed;
            chan2.LockscreenVisibility = NotificationVisibility.Public;
            Manager.CreateNotificationChannel(chan2);
        }

        private NotificationManager Manager
        {
            get
            {
                if (manager == null) manager = (NotificationManager) GetSystemService(NotificationService);
                return manager;
            }
        }

        //int SmallIcon => Android.Resource.Drawable.StatNotifyChat;
        private int SmallIcon => Resource.Drawable.icon;

        public Notification.Builder GetNotification1(string title, string body, PendingIntent contentIntent)
        {
            return new Notification.Builder(_context, PRIMARY_CHANNEL)
                .SetContentTitle(title)
                .SetContentText(body)
                .SetSmallIcon(SmallIcon)
                .SetAutoCancel(true)
                .SetContentIntent(contentIntent)
                .SetOnlyAlertOnce(true);
        }

        public Notification.Builder GetNotification2(string title, string body, PendingIntent contentIntent)
        {
            return new Notification.Builder(_context, SECONDARY_CHANNEL)
                .SetContentTitle(title)
                .SetContentText(body)
                .SetSmallIcon(SmallIcon)
                .SetContentIntent(contentIntent)
                .SetAutoCancel(true);
        }

        public void Notify(int id, Notification.Builder notification)
        {
            Manager.Notify(id, notification.Build());
        }
    }
}
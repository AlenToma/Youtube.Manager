using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Youtube.Manager.Helper;
using Youtube.Manager.Models.Container;

namespace Youtube.Manager.Droid.Services
{
    [Service(IsolatedProcess = false, Label = "YoutubeDownloadService")]
    public class FileService : Service
    {
        public static readonly string FileName = "Youtube.Manager.FileName";

        public static readonly string DirectoryName = "Youtube.Manager.DirectoryName";

        public static readonly string URL = "Youtube.Manager.URL";

        public static readonly string Title = "Youtube.Manager.Title";

        public IBinder Binder { get; private set; }

        public override IBinder OnBind(Intent intent)
        {
            Binder = new FileAcess(this);
            return Binder;
        }


        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            var NotificationHelper = new NotificationHelper(MainActivity.Current);
            var NotificationId = Methods.GetRandomId();
            var pendingIntent = PendingIntent.GetActivity(MainActivity.Current, NotificationId, intent,
                PendingIntentFlags.Immutable);
            var url = intent.GetStringExtra(URL);
            var fileName = intent.GetStringExtra(FileName);
            var directory = intent.GetStringExtra(DirectoryName);
            var title = intent.GetStringExtra(Title);

            intent.AddFlags(ActivityFlags.ClearTop);
            var nb = NotificationHelper.GetNotification1(GetString(Resource.String.Downloading) + " " + title,
                GetString(Resource.String.DownloadProgress), pendingIntent);


            if (Build.VERSION.SdkInt >= BuildVersionCodes.O) StartForeground(NotificationId, nb.Build());

            // dec DownloadCoins
            if (UserData.CurrentUser.UserType != UserType.Premium)
                UserData.CurrentUser.DownloadCoins -= 1;
            var _downlodFile = new DownlodFile(Methods.AppSettings.Logger, url, directory, fileName, procent =>
            {
                nb.SetContentText("")
                    .SetContentTitle(GetString(Resource.String.Downloading) + $" ({procent}%) " + title)
                    .SetProgress(100, procent, false);
                NotificationHelper.Notify(NotificationId, nb);
            }, async () =>
            {
                try
                {
                    await Methods.DownloadCompleted(fileName);
                }
                catch (Exception ex)
                {
                }

                nb.SetOnlyAlertOnce(false).SetProgress(0, 0, false)
                    .SetContentText(GetString(Resource.String.Finished));
                await Task.Delay(100);
                NotificationHelper.Notify(NotificationId, nb);
                ObjectCacher.RandomIdCacher.Remove(NotificationId);
                nb.Dispose();
                NotificationHelper.Dispose();
                if (Build.VERSION.SdkInt >= BuildVersionCodes.O) StopForeground(true);
            }, e =>
            {
                // faild no download was made, inc DownloadCoins
                if (UserData.CurrentUser.UserType != UserType.Premium)
                    UserData.CurrentUser.DownloadCoins += 1;
                nb.SetProgress(0, 0, false).SetContentText("Error: Something went wrong.\n Please try agen later.");
                NotificationHelper.Notify(NotificationId, nb);
                ObjectCacher.RandomIdCacher.Remove(NotificationId);
                nb.Dispose();
                NotificationHelper.Dispose();
                if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                    StopForeground(true);
            });
            _downlodFile.Download();

            NotificationHelper.Notify(NotificationId, nb);
            return StartCommandResult.Sticky;
        }
    }

    public class FileAcess : Binder
    {
        private readonly FileService service;

        public FileAcess(FileService service)
        {
            this.service = service;
        }

        public FileService GetNetworkService()
        {
            return service;
        }
    }
}
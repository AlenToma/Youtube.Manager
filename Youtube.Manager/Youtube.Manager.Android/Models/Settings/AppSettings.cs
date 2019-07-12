using Acr.UserDialogs;
using Android.Content;
using Android.Gms.Ads;
using Android.Gms.Ads.Reward;
using Android.OS;
using Android.Widget;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Youtube.Manager.Droid.Models.Ads.VideoAds;
using Youtube.Manager.Droid.Services;
using Youtube.Manager.Helper;
using Youtube.Manager.Models;
using Youtube.Manager.Models.Container;
using Youtube.Manager.Models.Container.Interface;

namespace Youtube.Manager.Droid.Models.Settings
{
    public class AppSettings : IMainActivity
    {
        private Context _context;

        protected IRewardedVideoAd mRewardedVideoAd;

        public AppSettings(Context context)
        {
            _context = context;
            MobileAds.Initialize(context, context.GetString(Resource.String.AdsApplicationIds)); // Ads
            mRewardedVideoAd = MobileAds.GetRewardedVideoAdInstance(context);
            mRewardedVideoAd.RewardedVideoAdListener = new RewardedVideoAdListener(mRewardedVideoAd);

            LocalMusicFolder = Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMusic).ToString(), "YoutubeManager");
            Logger = new Logger(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal));
            UserLocalSettings = FastDeepCloner.DeepCloner.CreateProxyInstance<LocalFileSettings>().ReadSettings(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Youtube.Manager.config"));

        }

        public string LocalMusicFolder { get; }

        public Logger Logger { get; }

        public LocalFileSettings UserLocalSettings { get; }

        public void DownloadVideo(YoutubeFileDownloadItem video)
        {
            if (!UserData.CanDownload())
            {
                Toast.MakeText(_context, "You dont have enough coins.", ToastLength.Long).Show();
            }

            var videos = ControllerRepository.Youtube(x => x.GetVideoAsync(video.VideoId, 18)).Await();
            if (!(videos?.Any() ?? false))
            {
                Toast.MakeText(_context, "Download unavailable", ToastLength.Long).Show();
                Logger?.Error($"Video [{video.VideoId}] unavailable");
                return;
            }

            if (!ObjectCacher.DownloadingFiles.ContainsKey(video.VideoId))
            {
                video.Auther = videos.First().Auther;
                video.Duration = videos.First().Duration;
                video.Description = videos.First().Description;
                video.Resolution = videos.First().Resolution;
                video.Size = videos.First().Size;
                video.FormatCode = videos.First().FormatCode;
                video.Quality = videos.First().Quality;
                ObjectCacher.DownloadingFiles.Add(video.VideoId, video);
                var intent = new Intent(_context, typeof(FileService));
                intent.PutExtra(FileService.URL, videos.FirstOrDefault()?.Url);
                intent.PutExtra(FileService.Title, video.Title);
                intent.PutExtra(FileService.DirectoryName, UserData.DirectoryManager.Folder(video.Playlist).Create().DirectoryPath);
                intent.PutExtra(FileService.FileName, video.GenerateLocalPath(videos.FirstOrDefault()));

                if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                    _context.StartForegroundService(intent);
                else
                    _context.StartService(intent);
             
            }
        }

        public Task<bool> IsOnline()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                return Task.FromResult(true);
            if (Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi))
                return Task.FromResult(true);

            return Task.FromResult(true);
        }

        public void OnLoginClick()
        {
            var intent = GoogleOAuthManager.Authenticator.GetUI(_context);
            _context.StartActivity(intent);
        }

        public void ReguastNewAdd()
        {
            var id = _context.GetString(Resource.String.RewardAddId);
#if DEBUG
            id = "ca-app-pub-3940256099942544/5224354917"; // TESTiD

#endif
            mRewardedVideoAd.LoadAd(id, new AdRequest.Builder().Build());
        }

        public async Task<bool> ValidateStoragePermission()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Storage);

            if (status != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Plugin.Permissions.Abstractions.Permission.Storage))
                {
                    await UserDialogs.Instance.AlertAsync("Need location", "The app wont work without having, you granting me permission to the music folder", "OK");
                }

                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Plugin.Permissions.Abstractions.Permission.Storage });
                status = results[Plugin.Permissions.Abstractions.Permission.Storage];
            }

            if (status == PermissionStatus.Granted)
            {
                if (UserData.CurrentUser != null && UserData.DirectoryManager == null)
                {
                    UserData.DirectoryManager = new DirectoryManager(LocalMusicFolder).Create().Folder(Actions.GenerateUserFolderName(UserData.CurrentUser.Email)).Create();
                }

                return true;

            }
            else if (status != PermissionStatus.Unknown)
            {
                return false;
                //location denied
            }

            return false;
        }
    }
}
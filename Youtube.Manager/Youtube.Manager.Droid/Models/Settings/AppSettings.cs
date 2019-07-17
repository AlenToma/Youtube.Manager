using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Gms.Ads;
using Android.Gms.Ads.Reward;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common.Apis;
using Android.OS;
using Android.Widget;
using Plugin.InAppBilling;
using Plugin.InAppBilling.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Youtube.Manager.Droid.Models.Ads.VideoAds;
using Youtube.Manager.Droid.Services;
using Youtube.Manager.Helper;
using Youtube.Manager.Models.Container;
using Youtube.Manager.Models.Container.DB_models;
using Youtube.Manager.Models.Container.DB_models.Library;
using Youtube.Manager.Models.Container.Interface;

namespace Youtube.Manager.Droid.Models.Settings
{
    public class AppSettings : IMainActivity
    {
        private readonly Context _context;

        protected IRewardedVideoAd mRewardedVideoAd;

        public string YoutubeDeveloperKey { get; }

        public Action<User, string> OnLoginComplete { get; set; }


        public static GoogleApiClient _googleApiClient { get; set; }



        public AppSettings(Context context)
        {
            _context = context;
            GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn).RequestEmail().Build();
            _googleApiClient = new GoogleApiClient.Builder(_context)
                .AddApi(Auth.GOOGLE_SIGN_IN_API, gso)
                .AddScope(new Scope(Android.Gms.Common.Scopes.Profile))
                .Build();
            YoutubeDeveloperKey = ControllerRepository.Db(x => x.GetSetting("YoutubeDeveloperKey")).Value;
            var adsApplicationIds = ControllerRepository.Db(x => x.GetSetting("AdsApplicationIds")).Value;
            MobileAds.Initialize(context, adsApplicationIds); // Ads
            mRewardedVideoAd = MobileAds.GetRewardedVideoAdInstance(context);
            mRewardedVideoAd.RewardedVideoAdListener = new RewardedVideoAdListener(mRewardedVideoAd);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity = context as Activity;
            LocalMusicFolder = Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMusic).ToString(), "YoutubeManager");
            Logger = new Logger(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal));
            UserLocalSettings = FastDeepCloner.DeepCloner.CreateProxyInstance<LocalFileSettings>().ReadSettings(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Youtube.Manager.config"));

        }

        public void OnAuthCompleted(GoogleSignInResult result)
        {
            if (result.IsSuccess)
            {
                GoogleSignInAccount accountt = result.SignInAccount;
                OnLoginComplete?.Invoke(new User()
                {
                    Email = accountt.Email,
                    Picture = accountt.PhotoUrl?.ToString()
                }, string.Empty);
            }
            else
            {
                OnLoginComplete?.Invoke(null, "An error occured!");
            }
        }

        internal void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            InAppBillingImplementation.HandleActivityResult(requestCode, resultCode, data);
            if (requestCode == 1)
            {
                GoogleSignInResult result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
                OnAuthCompleted(result); // todo later
            }
        }

        public string LocalMusicFolder { get; }

        public Logger Logger { get; }

        public LocalFileSettings UserLocalSettings { get; }

        public IntPtr Handle => throw new NotImplementedException();

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
            Intent signInIntent = Auth.GoogleSignInApi.GetSignInIntent(_googleApiClient);
            ((MainActivity)_context).StartActivityForResult(signInIntent, 1);
            _googleApiClient.Connect();

            //var intent = GoogleOAuthManager.Authenticator.GetUI(_context);
            //_context.StartActivity(intent);
        }

        public void ReguastNewAdd()
        {
            var id = ControllerRepository.Db(x => x.GetSetting("RewardAddId")).Value;
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

        public async Task<bool> CanPurchase()
        {
            if (!CrossInAppBilling.IsSupported)
                return false;

            try
            {
                var connected = await CrossInAppBilling.Current.ConnectAsync(ItemType.InAppPurchase);
                if (!connected)
                    return false;
                return true;

            }
            finally
            {
                await CrossInAppBilling.Current.DisconnectAsync();
            }
        }

        public async Task<bool> Buy(AppBillingProduct appBillingProduct)
        {
            var payLoad = "Youtube.Manager.Payload";
            if (!await CanPurchase())
                return false;

            var item = await CrossInAppBilling.Current.PurchaseAsync(appBillingProduct.ProductId, ItemType.InAppPurchase, payLoad);

            if (item == null || item.State != PurchaseState.Purchased)
                return false;

            await CrossInAppBilling.Current.ConsumePurchaseAsync(item.ProductId, item.PurchaseToken);

            UserData.CurrentUser.DownloadCoins += appBillingProduct.CoinsAmount;
            await UserData.SaveUserChanges();
            return true;
        }


        public async Task<List<AppBillingProduct>> GetProducts()
        {
            try
            {
                var products = ControllerRepository.Db(x => x.GetProductLinks(null));
                if (!products.Any() || !await CanPurchase())
                    return new List<AppBillingProduct>();
                var items = (await CrossInAppBilling.Current.GetProductInfoAsync(ItemType.InAppPurchase, products.Select(x => x.Product_Id).ToArray())).Where(x => products.Any(a => a.Product_Id == x.ProductId)).ToList().Astype<List<AppBillingProduct>>();
                items.ForEach(x => x.CoinsAmount = products.Find(a => a.Product_Id == x.ProductId).CoinsAmount);
                return items;
            }
            catch (Exception e)
            {
                Logger?.Error(e);
                return new List<AppBillingProduct>();
            }
        }



        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
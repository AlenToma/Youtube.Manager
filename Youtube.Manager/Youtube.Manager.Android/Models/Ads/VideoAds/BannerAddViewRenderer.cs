using Android.Content;
using Android.Gms.Ads;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Youtube.Manager.Controls.Ads;
using Youtube.Manager.Droid.Models.Ads.VideoAds;

[assembly: ExportRenderer(typeof(BannerAddView), typeof(BannerAddViewRenderer))]
namespace Youtube.Manager.Droid.Models.Ads.VideoAds
{

    public class BannerAddViewRenderer : ViewRenderer
    {

        private AdView adView;
        public BannerAddViewRenderer(Context context) : base(context)
        {

        }


        /// <summary>
        /// reload the view and hit up google admob 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);
            var id = MainActivity.Current.GetString(Resource.String.BannerAdd);
#if DEBUG
            id = "ca-app-pub-3940256099942544/6300978111"; // TESTiD

#endif
            adView = new AdView(Context);
            adView.AdSize = AdSize.Banner;
            adView.AdUnitId = id;
            var requestbuilder = new AdRequest.Builder();
            adView.LoadAd(requestbuilder.Build());
            SetNativeControl(adView);

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            adView?.Dispose();
        }
    }
}
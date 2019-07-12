using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Youtube.Manager.Models;

namespace Youtube.Manager.Droid
{
    // Activity in Droid project (this is separate from your Main activity)
    [Activity(Label = "OAuth2Activity", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
    [IntentFilter(
        new[] {Intent.ActionView},
        Categories = new[] {Intent.CategoryBrowsable, Intent.CategoryDefault},
        DataScheme = GoogleOAuthManager.REDIRECT_SCHEME,
        DataPath = GoogleOAuthManager.REDIRECT_PATH
    )]
    public class OAuth2Activity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var uri = new Uri(Intent.Data.ToString());
            GoogleOAuthManager.Authenticator.OnPageLoading(uri);

            Finish();
            var intent = new Intent(this, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            StartActivity(intent);
        }
    }
}
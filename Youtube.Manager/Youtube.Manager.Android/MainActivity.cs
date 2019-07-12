using System;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using MediaManager;
using Rg.Plugins.Popup;
using Xamarin.Auth;
using Xamarin.Auth.Presenters.XamarinAndroid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Youtube.Manager.Droid.Models;
using Youtube.Manager.Helper;
using Plugin.Permissions;
using Android.Runtime;
using Acr.UserDialogs;
using Youtube.Manager.Droid.Models.Settings;

namespace Youtube.Manager.Droid
{
    [Activity(
        Label = "Youtube.Manager",
        Icon = "@mipmap/icon",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        LaunchMode = LaunchMode.SingleTop,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation
    )]
    public class MainActivity : FormsAppCompatActivity
    {
        private const int PickImageId = 1000;

        public static MainActivity Current { private set; get; }


        private readonly TaskCompletionSource<string> _pickImageTaskCompletionSource = new TaskCompletionSource<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Current = this;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);
            Popup.Init(this, savedInstanceState); // popup 
            Forms.SetFlags("Shell_Experimental", "Visual_Experimental", "CollectionView_Experimental");
            Forms.Init(this, savedInstanceState);
            AuthenticationConfiguration.Init(this, savedInstanceState);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);
            UserDialogs.Init(this);
            CustomTabsConfiguration.CustomTabsClosingMessage = null;
            // Media player
            CrossMediaManager.Current.Init();
            Methods.AppSettings = new AppSettings(this);
            LoadApplication(new App());
    
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private ContextMenuRenderer _selectedMenu;
        public override bool OnContextItemSelected(IMenuItem item)
        {
            if (_selectedMenu != null)
                _selectedMenu.ItemClick?.Invoke(_selectedMenu.Menus.FirstOrDefault(x => x.Text == item.ToString()));
            return base.OnContextItemSelected(item);
        }

        public override void OnCreateContextMenu(IContextMenu menu, Android.Views.View v, IContextMenuContextMenuInfo menuInfo)
        {
            _selectedMenu = v.Parent.Parent.Parent as ContextMenuRenderer;
            base.OnCreateContextMenu(menu, v, menuInfo);
            if (_selectedMenu != null)
            {
                if (!string.IsNullOrEmpty(_selectedMenu.Title))
                    menu.SetHeaderTitle(_selectedMenu.Title);
                foreach (var item in _selectedMenu.Menus)
                {
                    var m = menu.Add(item.Text);
                    var icon = ContextMenuRenderer.GetIcon(item.Image);
                    if (icon != null)
                        m.SetIcon(icon);
                }
            }
        }

        public override void OnBackPressed()
        {
            if (Popup.SendBackPressed(base.OnBackPressed))
            {
                //Debug.WriteLine("Android back button: There are some pages in the PopupStack");
            }
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            ObjectCacher.Clear();
        }

        // this is for youtube player
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == PickImageId)
            {
                if (resultCode == Result.Ok && data != null)
                    _pickImageTaskCompletionSource.SetResult(data.DataString);
                else
                    _pickImageTaskCompletionSource.SetResult(null);
            }
        }
    }
}
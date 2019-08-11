using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Realm.Of.Y.Manager.Controls;
using Realm.Of.Y.Manager.Droid.Models;
using ARelativeLayout = Android.Widget.RelativeLayout;

[assembly: ExportRenderer(typeof(ContextView), typeof(ContextMenuRenderer))]

namespace Realm.Of.Y.Manager.Droid.Models
{
    public class ContextMenuRenderer : ViewRenderer<ContextView, ARelativeLayout>
    {
        public Action<Controls.MenuItem> ItemClick { get => Element.Click; }
        public List<Controls.MenuItem> Menus { get => Element?.Menus; }

        private Android.Widget.ImageButton _imageButton;

        public string Title { get => Element?.Title; }

        public ContextMenuRenderer(Context context) : base(context)
        {

        }

        protected override void Dispose(bool disposing)
        {
            MainActivity.Current.UnregisterForContextMenu(_imageButton);
            base.Dispose(disposing);
        }

        public static Drawable GetIcon(ImageSource icon)
        {
            // Get the filename from the ImageSource
            var fileImage = (FileImageSource)icon;

            if (fileImage == null)
            {
                return null;
            }
            var file = fileImage.File?.Split('.').FirstOrDefault();

            if (file == null)
                return null;

            // Get the identifier (resource ID for the filename
            int drawableId = MainActivity.Current.Resources.GetIdentifier(file, "drawable", MainActivity.Current.PackageName);

            // Get the drawable for the resource Id
            var drawable = ContextCompat.GetDrawable(MainActivity.Current, drawableId);
            //drawable.SetBounds(
            //    0,
            //    0,
            //    (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 24f, MainActivity.Current.Resources.DisplayMetrics),
            //    (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 24f, MainActivity.Current.Resources.DisplayMetrics));
            return drawable;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ContextView> e)
        {
            base.OnElementChanged(e);

            var vi = LayoutInflater.From(Context);
            var controller = vi.Inflate(Resource.Layout.contextMenu, null);
            var relativeLayout = new ARelativeLayout(Context);
            relativeLayout.AddView(controller);
            SetNativeControl(relativeLayout);

            _imageButton = controller.FindViewById<Android.Widget.ImageButton>(Resource.Id.btnShow);
            if (Element.Width > 0)
                _imageButton.SetMaxWidth((int)Element.Width);
            if (Element.Height > 0)
                _imageButton.SetMaxHeight((int)Element.Width);
            MainActivity.Current.RegisterForContextMenu(_imageButton);

            _imageButton.Touch += (sender, args) =>
            {
                args.Handled = false;

                switch (args.Event.Action)
                {
                    case MotionEventActions.Up:
                    case MotionEventActions.Cancel:
                        Element.BackgroundColor = Color.Transparent;
                        break;

                    case MotionEventActions.Down:
                        Element.BackgroundColor = Color.WhiteSmoke;

                        break;
                }
            };
        }
    }
}
using System.ComponentModel;
using Android.Content;
using Android.Support.V4.Content;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Realm.Of.Y.Manager.Controls;
using Realm.Of.Y.Manager.Droid.Models;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer))]

namespace Realm.Of.Y.Manager.Droid.Models
{
    public class CustomButtonRenderer : ButtonRenderer
    {
        private readonly Context _context;

        public CustomButtonRenderer(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var control = Control;
            var customRendererButton = sender as CustomButton;
            if (!customRendererButton.IsEnabled)
                control.SetBackgroundColor(new Color(
                    ContextCompat.GetColor(_context, Resource.Color.disabled_background)
                ));

            switch (customRendererButton.TextAlignment)
            {
                case Xamarin.Forms.TextAlignment.Center:
                    control.Gravity = GravityFlags.Center | GravityFlags.CenterVertical;
                    break;
                case Xamarin.Forms.TextAlignment.Start:
                    control.Gravity = GravityFlags.Left | GravityFlags.CenterVertical;

                    break;
                case Xamarin.Forms.TextAlignment.End:
                    control.Gravity = GravityFlags.End | GravityFlags.CenterVertical;
                    break;
            }

            if (e != null)
                base.OnElementPropertyChanged(sender, e);
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }


        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            var customRendererButton = e.NewElement as CustomButton;

            var control = Control;

            var selected = customRendererButton.IsSelected;
            var onSelectColor = customRendererButton.OnPressColor;
            control.Touch += (sender, args) =>
            {
                args.Handled = false;

                switch (args.Event.Action)
                {
                    case MotionEventActions.Up:
                    case MotionEventActions.Cancel:
                        Element.ScaleTo(1, 50, Easing.CubicIn);
                        break;

                    case MotionEventActions.Down:
                        Element.ScaleTo(0.8, 50, Easing.CubicOut);

                        break;
                }
            };

            OnElementPropertyChanged(customRendererButton, null);
        }


        private bool isPointWithin(int x, int y, int x1, int x2, int y1, int y2)
        {
            return x <= x2 && x >= x1 && y <= y2 && y >= y1;
        }
    }
}
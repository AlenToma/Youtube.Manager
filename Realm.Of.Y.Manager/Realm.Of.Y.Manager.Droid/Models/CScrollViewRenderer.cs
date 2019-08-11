using Android.Content;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Realm.Of.Y.Manager.Controls;
using Realm.Of.Y.Manager.Droid.Models;

[assembly: ExportRenderer(typeof(CScrollView), typeof(CScrollViewRenderer))]

namespace Realm.Of.Y.Manager.Droid.Models
{
    public class CScrollViewRenderer : ScrollViewRenderer
    {
        public static bool IsDisabled;

        private int oldx;
        private int oldy;

        public CScrollViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
        }

        protected override void OnScrollChanged(int l, int t, int oldl, int oldt)
        {
            if (IsDisabled && oldx != l && t != oldy)
            {
                oldx = oldl;
                oldy = oldt;
                ScrollTo(oldl, oldt);
                return;
            }

            if (!IsDisabled)
            {
                base.OnScrollChanged(l, t, oldl, oldt);
            }

            oldx = -1;
            oldy = -1;
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            return base.OnInterceptTouchEvent(ev);
        }
    }
}
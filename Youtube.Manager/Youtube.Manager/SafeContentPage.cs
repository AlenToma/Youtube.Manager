using System;
using Xamarin.Forms;
using Youtube.Manager.Helper;
using Youtube.Manager.Models.Container;

namespace Youtube.Manager
{
    public class SafeContentPage : BaseContentPage
    {
        private double _height;

        private double _width;
        protected PageOrientation? PageO;

        public SafeContentPage()
        {
            BackgroundColor = (Color) Application.Current.Resources["applicationColor"];
            NavigationPage.SetHasNavigationBar(this, false);
            this.LoadValueConverters();
        }

        protected virtual void OnOrientationChanged(PageOrientation pageOrientation)
        {
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            var oldWidth = _width;
            const double sizenotallocated = -1;

            base.OnSizeAllocated(width, height);
            if (Equals(_width, width) && Equals(_height, height)) return;

            _width = width;
            _height = height;

            // ignore if the previous height was size unallocated
            if (Equals(oldWidth, sizenotallocated)) return;
            PageO = width < height ? PageOrientation.Vertical : PageOrientation.Horizontal;
            // Has the device been rotated ?
            if (!Equals(width, oldWidth))
                OnOrientationChanged(PageO.Value);
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            if (Parent == null)
                DisposeBindingContext();
        }

        protected void DisposeBindingContext()
        {
            if (BindingContext is IDisposable disposableBindingContext)
            {
                disposableBindingContext.Dispose();
                BindingContext = null;
            }
        }

        ~SafeContentPage()
        {
            DisposeBindingContext();
        }
    }
}
using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Youtube.Manager.Helper;
using Youtube.Manager.Models.Container;
using Youtube.Manager.Models.Container.DB_models;
using Youtube.Manager.Models.Container.Interface;

namespace Youtube.Manager
{
    public class PopupBase : PopupPage, ModuleTrigger
    {
        private double _height;
        private double _width;
        protected PageOrientation? PageO;

        public PopupBase()
        {
            this.LoadValueConverters();
            //this.BackgroundColor = (Color)Application.Current.Resources["applicationColor"];
            //NavigationPage.SetHasNavigationBar(this, false);
        }

        public virtual async Task DataBinder(MethodInformation method = null)
        {
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

        protected override void OnDisappearing()
        {
            this.RemoveTrigger();
            base.OnDisappearing();
        }

        protected void DisposeBindingContext()
        {
            if (BindingContext is IDisposable disposableBindingContext)
            {
                disposableBindingContext.Dispose();
                BindingContext = null;
            }
        }

        ~PopupBase()
        {
            DisposeBindingContext();
        }
    }
}
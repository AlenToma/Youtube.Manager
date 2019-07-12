using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

namespace Youtube.Manager.Views
{
    public partial class LoadingPopupPage : PopupPage
    {
        public LoadingPopupPage()
        {
            InitializeComponent();
        }

        private async void OnClose(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        public async Task Open()
        {
            await PopupNavigation.Instance.PushAsync(this, true);
        }

        public async Task Close()
        {
            await PopupNavigation.Instance.RemovePageAsync(this, true);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
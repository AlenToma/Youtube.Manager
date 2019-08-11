using Rg.Plugins.Popup.Pages;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Realm.Of.Y.Manager.Helper;
using Realm.Of.Y.Manager.Models.Container.DB_models.Library;

namespace Realm.Of.Y.Manager.Views.Template
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuyCoins : PopupPage
    {
        public BuyCoins()
        {
            InitializeComponent();
            DataBind();
        }

        private async void DataBind()
        {
            var lo = await this.StartLoading();
            var products = await Methods.AppSettings.GetProducts();
            if (products.Any())
            {
                _products.ItemsSource = products;
            }
            else _products.IsVisible = false;

            lo.EndLoading();
        }

        private async void _btnStartAds_Clicked(object sender, EventArgs e)
        {
            Methods.ApplicationPlayer?.Player?.Stop();
            var lo = await this.StartLoading();
            Methods.AppSettings.ReguastNewAdd();
            await this.Close();
            lo.EndLoading();
        }

        private async void _btnBuy_Clicked(object sender, EventArgs e)
        {
            var product = ((Button)sender).BindingContext as AppBillingProduct;
            var r = await Methods.AppSettings.Buy(product);
            if (r)
                await this.Close();

            await Application.Current.MainPage.DisplayAlert("", "Something went wrong, the operation did not failed. \n Please try agen later", "Ok".GetString());
        }
    }
}
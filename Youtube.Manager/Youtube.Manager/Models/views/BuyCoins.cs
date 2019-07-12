using Rg.Plugins.Popup.Pages;
using System;
using Xamarin.Forms;
using Youtube.Manager.Controls;
using Youtube.Manager.Helper;

namespace Youtube.Manager.Models.views
{
    public class BuyCoins
    {

        public BuyCoins()
        {
            var stk = new StackLayout()
            {
                Style = (Style)Application.Current.Resources["PopUpCenter"],
                Children =
                        {
                            new StackLayout
                            {
                                Style = (Style) Application.Current.Resources["FormFloatLeft"],
                                Orientation= StackOrientation.Vertical,
                                HorizontalOptions= LayoutOptions.FillAndExpand,
                                VerticalOptions = LayoutOptions.FillAndExpand,
                                BackgroundColor = (Color) Application.Current.Resources["barBackgroundColor"],
                            },
                    }
            };

            var view = new PopupPage
            {
                Content = stk
            };
            var btnAds = new CustomButton()
            {
                Padding = new Thickness(0, 0, 0, 0),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Style = (Style)Application.Current.Resources["ButtonContainer"],
                TextColor = (Color)Application.Current.Resources["text"],
                IsSelected = true,
                Text = "AdsButton".GetString(),
                ImageSource = ImageSource.FromFile("coinCounter.png"),
                FontSize = 10
            };

            btnAds.Clicked += new EventHandler(async (sender, e) =>
            {
                var lo = await view.StartLoading();
                Methods.AppSettings.ReguastNewAdd();
                await view.Close();
                lo.EndLoading();
            });

            var btnBuy = new CustomButton()
            {
                Padding = new Thickness(0, 0, 0, 0),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Style = (Style)Application.Current.Resources["ButtonContainer"],
                TextColor = (Color)Application.Current.Resources["text"],
                Text = $"{"Buy".GetString()} {"Amount".GetString()} coins for {"AmountMony".GetString()} {"USD".GetString()}",
                IsSelected = true,
                ImageSource = ImageSource.FromFile("coinCounter.png"),
                FontSize = 10
            };

            btnBuy.Clicked += new EventHandler(async (sender, e) =>
            {
                //Methods.AppSettings.ReguastNewAdd(); // todo implement buy form
                await view.Close();
            });
            (stk.Children[0] as StackLayout).Children.Add(btnAds);
            (stk.Children[0] as StackLayout).Children.Add(btnBuy);

            view.Open();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Realm.Of.Y.Manager.Controls
{
    public class LoaderIndicator : StackLayout
    {

        public new bool IsVisible { get => base.IsVisible; set { base.IsVisible = value; this.Children[0].IsEnabled = value; } }
        public LoaderIndicator()
        {


            var loader = new ActivityIndicator()
            {
                Color = Color.Red,
                IsRunning = true,
                IsEnabled = true,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                HeightRequest = 40,
                WidthRequest = 40
            };

            this.Children.Add(loader);

        }
    }
}

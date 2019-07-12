using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Youtube.Manager.Controls
{
    public class LoaderIndicator : StackLayout
    {

        public new bool IsVisible { get => base.IsVisible; set { base.IsVisible = value; this.Children[0].IsEnabled = value; } }
        public LoaderIndicator()
        {


            var loader = new ActivityIndicator()
            {
                Color = Color.White,
                IsRunning = true,
                IsEnabled = true,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                HeightRequest = 70,
                WidthRequest = 70
            };

            this.Children.Add(loader);

        }
    }
}

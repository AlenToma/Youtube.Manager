using System;
using Xamarin.Forms;
using Realm.Of.Y.Manager.Models;
using Realm.Of.Y.Manager.Models.Container;

namespace Realm.Of.Y.Manager.Controls
{

    public class ToolBarItem
    {
        public string Identifier { get; set; }

        public ImageSource Icon { get; set; }

        public string Text { get; set; }

        public bool IsVisiable { get; set; }

        public Float Float { get; set; }

        public double Width { get; set; } = -1;
    }

    public class Toolbar : StackLayout
    {
        public event Action<ToolBarItem, CustomButton> ItemClicked;
        public GenericContent<ToolBarItem> ToolBarItems { get; set; }

        public Toolbar()
        {
            ToolBarItems = new GenericContent<ToolBarItem>(Build, Refresh);
            this.Style = (Style)Application.Current.Resources["FormFloatLeft"];
            this.HeightRequest = 25;
            this.BackgroundColor = (Color)Application.Current.Resources["barBackgroundColor"];
            this.Padding = new Thickness(0);
            this.VerticalOptions = LayoutOptions.Start;
        }

        public void Refresh()
        {
            this.Children.Clear();
            ToolBarItems?.ForEach(x => Build(x));
        }

        public ToolBarItem GetItemByIdentifier(string identifier)
        {
            return ToolBarItems.Find(x => string.Equals(identifier, x.Identifier));
        }

        public void Build(ToolBarItem item)
        {
            var btn = new CustomButton
            {
                Padding = new Thickness(0, 0, 0, 0),
                HorizontalOptions = item.Float == Float.Left ? LayoutOptions.Start : LayoutOptions.End,
                Style = (Style)Application.Current.Resources["ButtonContainer"],
                TextColor = (Color)Application.Current.Resources["text"],
                WidthRequest = item.Width,
                HeightRequest = this.Height

            };

            if (!string.IsNullOrWhiteSpace(item.Text))
            {
                btn.Text = item.Text;
                btn.Padding = new Thickness(1, 0, 1, 0);
                btn.FontSize = 12;
                btn.TextAlignment = TextAlignment.Start;
            }
            else btn.FontSize = 0;

            if (item.Icon != null)
            {
                btn.ImageSource = item.Icon;

            }
            if (ItemClicked != null)
                btn.Clicked += new EventHandler((sender, e) => ItemClicked.Invoke(item, btn));

            this.Children.Add(btn);
        }

    }
}

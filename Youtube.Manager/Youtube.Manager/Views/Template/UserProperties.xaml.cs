using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Youtube.Manager.Controls;
using Youtube.Manager.Helper;

namespace Youtube.Manager.Views.Template
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProperties : ContentView
    {
        public UserProperties()
        {
            InitializeComponent();
        }

        private async void BtnLogs_ClickedAsync(object sender, EventArgs e)
        {
            var horizontalList = new HorizontalList();
            horizontalList.ItemTemplate = new DataTemplate(() =>
            {


                var stk = new StackLayout { Style = (Style)Application.Current.Resources["FormFloatLeft"] };
                var lblNr = new Label
                {
                    Style = (Style)Application.Current.Resources["Log"],
                    HorizontalOptions = LayoutOptions.Start,
                    MinimumWidthRequest = 30
                };
                lblNr.SetBinding(Label.TextProperty, "Number");

                var lblText = new Label
                {
                    Style = (Style)Application.Current.Resources["Log"]
                };
                lblText.SetBinding(Label.TextProperty, "Content");

                stk.Children.Add(lblNr);
                stk.Children.Add(lblText);
                return stk;
            });

            var popUpPage = new PopupBase();
            popUpPage.Content = horizontalList;
            popUpPage.BackgroundColor = Color.Blue;
            horizontalList.ItemsSource = Methods.AppSettings.Logger.GetLog().Select((x, i) => new { Number = i + 1, Content = x }).ToList();

            horizontalList.SelectedItemChanged += new EventHandler(async (s, o) =>
            {
                var pop = new PopupBase();
                var grid = new Grid
                {
                    BackgroundColor = Color.White,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Padding= 5
                };
                var scroll = new ScrollView()
                {
                    Orientation = ScrollOrientation.Vertical,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };
                grid.RowDefinitions.Add(new RowDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                Label ed = new Label()
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.Start,
                    Style = (Style)Application.Current.Resources["BaseText"],
                    
                };
                scroll.Content = grid;
                grid.Children.Add(ed, 0, 0);
                pop.Content = scroll;
                pop.Appearing += new EventHandler((ss, oo) => { ed.Text = (horizontalList.SelectedItem as dynamic).Content; });
                pop.Open();
            });
            popUpPage.Open();
        }

        private async void _btnChangeLogin_Clicked(object sender, EventArgs e)
        {
           await UserData.LogIn(_txtEmail.Text);
        }
    }
}
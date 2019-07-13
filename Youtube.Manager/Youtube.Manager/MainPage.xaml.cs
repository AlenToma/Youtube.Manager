using dotMorten.Xamarin.Forms;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Youtube.Manager.Helper;
using Youtube.Manager.Models.Container;
using Youtube.Manager.Models.Container.Interface;
using Youtube.Manager.Models.Container.Interface.API;
using Youtube.Manager.Views;
using Youtube.Manager.Views.Template;

namespace Youtube.Manager
{
    public partial class MainPage : SafeContentPage, ModuleTrigger
    {
        public MainPage()
        {
            InitializeComponent();
            BackgroundColor = (Color)Application.Current.Resources["applicationColor"];
            this.AddTrigger(ControllerRepository.GetInfo<IDbController, Task>(x => x.SaveVideo(null)))
             .AddTrigger(ControllerRepository.GetInfo<IDbController, Task>(x => x.SaveCategory(null)))
             .AddTrigger(ControllerRepository.GetInfo<IDbController, Youtube.Manager.Models.Container.DB_models.User>(x => x.SaveUser(null)))
            .AddTrigger(ControllerRepository.GetInfo<IDbController, Models.Container.DB_models.User>(x => x.LogIn(null, null, null)));
        }


        public async Task DataBinder(MethodInformation method = null)
        {
            if (!this.UserIsLogedIn())
                return;
            if (UserData.CurrentUser.UserType == UserType.User)
                toolbar.GetItemByIdentifier("DownloadCoin").Text = UserData.CurrentUser.DownloadCoins.ToString("C2");
            else toolbar.GetItemByIdentifier("DownloadCoin").Text = "Infinity".GetString();
            toolbar.Refresh();
        }

        private void _txtSearch_TextChanged(object sender, dotMorten.Xamarin.Forms.AutoSuggestBoxTextChangedEventArgs e)
        {
            if (e.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                Task.Run(async () =>
                {
                    var source = ControllerRepository.Youtube(x => x.GetSuggestQueries(_txtSearch.Text)).Await()?.ToList();
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _txtSearch.ItemsSource = source;
                    });
                });

            }
        }

        private async void _txtSearch_QuerySubmitted(object sender, AutoSuggestBoxQuerySubmittedEventArgs e)
        {
            if (!await Methods.AppSettings.ValidateStoragePermission() || string.IsNullOrEmpty(_txtSearch.Text))
                return;

            var text = _txtSearch.Text;
            if (e?.ChosenSuggestion != null)
                text = e.ChosenSuggestion.ToString();

            if (string.IsNullOrWhiteSpace(text))
                return;

            var l = await this.StartLoading();
            var videos = ControllerRepository.Youtube(x => x.SearchAsync(UserData.CurrentUser.EntityId.Value, text, 6, 1, null, VideoSearchType.All)).Await();
            var src = new SearchView(videos, _txtSearch.Text);
            src.Open();
            l.EndLoading();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            _txtSearch_QuerySubmitted(null, null);
        }

        private void Toolbar_ItemClicked(Controls.ToolBarItem arg1, Controls.CustomButton arg2)
        {
            switch (arg1.Identifier)
            {
                case "DownloadCoin":
                    new BuyCoins().Open();
                    break;
            }
        }


    }
}
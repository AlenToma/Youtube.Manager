using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Auth;
using Youtube.Manager.Helper;
using Youtube.Manager.Models;
using Youtube.Manager.Models.Container;
using Youtube.Manager.Models.Container.DB_models;
using Youtube.Manager.Models.Container.Interface.API;

namespace Youtube.Manager.Views
{
    public partial class User : PopupBase
    {
        public User()
        {
            this.AddTrigger(
            ControllerRepository.GetInfo<IDbController, Models.Container.DB_models.User>(x => x.LogIn(null, null, null)));
            InitializeComponent();
            GoogleOAuthManager.Authenticator.Completed += Authenticator_Completed;
            GoogleOAuthManager.Authenticator.Error += Authenticator_Error;
            _btnGoogle_Clicked();
        }




        public override Task DataBinder(MethodInformation method = null)
        {

            userSettings.IsVisible = this.UserIsLogedIn();

            return base.DataBinder(method);
        }

        private async void Authenticator_Error(object sender, AuthenticatorErrorEventArgs e)
        {
            if (UserData.CurrentUser?.EntityId == null) // validate CurrentUser
            {
                await DisplayAlert("", "GoogleAuthenticationError".GetString(), "Ok".GetString());
                await _btnGoogle_Clicked(); // try agen
            }
        }

        private async void Authenticator_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                var request = new OAuth2Request("GET", new Uri(ApiControllerMapping.GoogleUserInfo), null, e.Account);
                var response = await request.GetResponseAsync();
                if (response != null)
                {
                    var userJson = response.GetResponseText();
                    var user = JsonConvert.DeserializeObject<Models.Container.DB_models.User>(userJson);
                    if (user != null)
                        await UserData.LogIn(user.Email, "google.com", user.Picture);

                    Authenticator_Error(null, null);
                }
            }
            else await _btnGoogle_Clicked();
        }

        private async Task _btnGoogle_Clicked()
        {
            if (this.UserIsLogedIn()) // SafeGard
                return;

            if (!string.IsNullOrEmpty(Methods.AppSettings.UserLocalSettings.UserName) && !string.IsNullOrEmpty(Methods.AppSettings.UserLocalSettings.Password))
            {
                var loader = await this.StartLoading();
                await UserData.LogIn(Methods.AppSettings.UserLocalSettings.UserName, Methods.AppSettings.UserLocalSettings.Password);
                loader.EndLoading();
            }
            if (!this.UserIsLogedIn()) // still failed, then use google api
            {
                Methods.AppSettings.UserLocalSettings.UserName = string.Empty;
                Methods.AppSettings.UserLocalSettings.Password = string.Empty;
                Methods.AppSettings.UserLocalSettings.Image = string.Empty;
                PageManager.OnLogIn();
            }
        }
    }
}
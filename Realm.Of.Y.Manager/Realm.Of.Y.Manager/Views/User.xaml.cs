using Rest.API.Translator;
using System.Threading.Tasks;
using Realm.Of.Y.Manager.Helper;
using Realm.Of.Y.Manager.Models.Container;
using Realm.Of.Y.Manager.Models.Container.Interface.API;

namespace Realm.Of.Y.Manager.Views
{
    public partial class User : PopupBase
    {
        public User()
        {
            this.AddTrigger(
            ControllerRepository.GetInfo<IDbController, Models.Container.DB_models.User>(x => x.LogIn(null, null, null)));
            InitializeComponent();
            Methods.AppSettings.OnLoginComplete = async (u, e) =>
            {
                if (u != null)
                {
                    await UserData.LogIn(u.Email, "google.com", u.Picture);

                    Authenticator_Error();

                }
                else await _btnGoogle_Clicked();
            };
            _btnGoogle_Clicked();
        }




        public override async Task DataBinder(MethodInformation method = null)
        {

            userSettings.IsVisible = this.UserIsLogedIn();

            if (this.UserIsLogedIn())
                await Methods.AppSettings.ValidateStoragePermission();
           await base.DataBinder(method);
        }

        private async void Authenticator_Error()
        {
            if (UserData.CurrentUser?.EntityId == null) // validate CurrentUser
            {
                await DisplayAlert("", "GoogleAuthenticationError".GetString(), "Ok".GetString());
                await _btnGoogle_Clicked(); // try agen
            }
        }

        private async Task _btnGoogle_Clicked()
        {
            if (this.UserIsLogedIn()) // SafeGard
                return;

            if (!string.IsNullOrEmpty(Methods.AppSettings.UserLocalSettings.UserName) && !string.IsNullOrEmpty(Methods.AppSettings.UserLocalSettings.Password))
            {
                var loader = await this.StartLoading();
                await UserData.LogIn(Methods.AppSettings.UserLocalSettings.UserName, Methods.AppSettings.UserLocalSettings.Password);

                if (!this.UserIsLogedIn()) // SafeGard
                {
                    Methods.AppSettings.UserLocalSettings.UserName = string.Empty;
                    _btnGoogle_Clicked();
                }
                loader.EndLoading();
            }
            if (!this.UserIsLogedIn()) // still failed, then use google api
            {
                Methods.AppSettings.UserLocalSettings.UserName = string.Empty;
                Methods.AppSettings.UserLocalSettings.Password = string.Empty;
                Methods.AppSettings.UserLocalSettings.Image = string.Empty;
                Methods.AppSettings.OnLoginClick();
            }
        }
    }
}
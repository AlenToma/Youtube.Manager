using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Realm.Of.Y.Manager.Helper;

namespace Realm.Of.Y.Manager
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {
            this.LoadValueConverters();
            BackgroundColor = (Color) Application.Current.Resources["applicationColor"];
        }
    }
}
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Youtube.Manager.Helper;

namespace Youtube.Manager
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
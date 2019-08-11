using Xamarin.Forms;

namespace Realm.Of.Y.Manager.Helper
{
    public static class ResourcesExtension
    {
        public static string GetString(this string key)
        {
            return Application.Current.Resources[key].ToString();
        }
    }
}
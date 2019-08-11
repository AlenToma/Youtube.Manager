using System;
using System.Globalization;
using Xamarin.Forms;
using Realm.Of.Y.Manager.Models.Container.Attributes;

namespace Realm.Of.Y.Manager.Controls.Converter
{
    [ClassKey("Invert")]
    internal class InvertBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool) value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
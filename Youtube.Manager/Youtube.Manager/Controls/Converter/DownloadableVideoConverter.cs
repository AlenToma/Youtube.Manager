using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Youtube.Manager.Models.Container;
using Youtube.Manager.Models.Container.Attributes;
using Youtube.Manager.Models.Container.DB_models.Library;

namespace Youtube.Manager.Controls.Converter
{
    [ClassKey("Downloadable")]
    public class DownloadableVideoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value as VideoWrapper;
            if (v == null)
                return true;

            var task = !v.IsVideo ? true : !(UserData.DirectoryManager.GetFile(v.Id, SearchOption.AllDirectories) != null);
            return !(parameter?.ToString().Contains("Invert") ?? false) ? task : !task;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
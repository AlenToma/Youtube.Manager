using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;
using Youtube.Manager.Helper;
using Youtube.Manager.Models.Container.Attributes;
using Youtube.Manager.Models.Container.DB_models;
using Youtube.Manager.Models.Container.DB_models.Library;

namespace Youtube.Manager.Controls.Converter
{
    [ClassKey("ImageSource")]
    public class ImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var video = value as VideoWrapper;

            if (video != null) return GetImage(video.DefaultThumbnailUrl);

            var videoData = value as VideoData;
            if (videoData != null)
                return GetImage(videoData.ThumpUrl);

            return GetImage(value?.ToString() ?? "x");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private ImageSource GetImage(object value)
        {
            if (ObjectCacher.CachedImages.ContainsKey(value?.ToString()))
                return GetImage(ObjectCacher.CachedImages[value?.ToString()]);

            if (value is string && value != null && value.ToString().Length >= 4 && !value.ToString().Contains("http"))
            {
                var Base64Stream = System.Convert.FromBase64String(value.ToString());
                var img = ImageSource.FromStream(() => new MemoryStream(Base64Stream));
                return img;
            }

            if (value is string && !string.IsNullOrEmpty(value?.ToString() ?? "") && value.ToString().Contains("http"))
            {
                var data = Models.Container.HttpHelper.GetImage(value.ToString());
                ObjectCacher.CachedImages.Add(value.ToString(), data);
                return ImageSource.FromStream(() => new MemoryStream(data));
            }

            if (value is byte[]) return ImageSource.FromStream(() => new MemoryStream(value as byte[]));
            return ImageSource.FromFile("NoImage.png");
        }
    }
}
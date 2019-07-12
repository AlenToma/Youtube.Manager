using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Youtube.Manager.Core;

namespace Youtube.Manager.API.Models
{
    public static class Helper
    {
        public static JsonNetResult JsonResult<T>(this T item, HttpRequestMessage request)
        {
            return new JsonNetResult(item, new JsonSerializerSettings(), Encoding.UTF8, request);
        }

        public static async Task<JsonNetResult> JsonResultAsync<T>(this T item, HttpRequestMessage request)
        {
            return await Task.FromResult(new JsonNetResult(item, new JsonSerializerSettings(), Encoding.UTF8, request));
        }

        public static Bitmap GetImage(string filename)
        {
            using (var client = new WebClient())
            {
                var stream = client.OpenRead(filename);
                Bitmap bitmap;
                bitmap = new Bitmap(stream);
                return bitmap;
            }
        }

        public static Bitmap GenerateThumbImage(Bitmap image, int newWidth, int newHeight)
        {
            try
            {
                float ratio = 1;
                float minSize = Math.Min(newHeight, newHeight);

                var srcBmp = image;
                if (srcBmp.Width > srcBmp.Height)
                    ratio = minSize / srcBmp.Width;
                else
                    ratio = minSize / srcBmp.Height;

                var newSize = new SizeF(srcBmp.Width * ratio, srcBmp.Height * ratio);
                var target = new Bitmap((int) newSize.Width, (int) newSize.Height);

                using (var graphics = Graphics.FromImage(target))
                {
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.DrawImage(srcBmp, 0, 0, newSize.Width, newSize.Height);
                    return target;
                    //using (MemoryStream memoryStream = new MemoryStream())
                    //{
                    //    target.Save(memoryStream, ImageFormat.Jpeg);
                    //    return memoryStream.ToArray();
                    //}
                }
            }
            catch
            {
                return image;
            }
        }


        public static byte[] CombineImages(params string[] imageUrl)
        {
            var staticWidth = 200;
            var staticHeight = 200;
            var images = imageUrl
                .Select((x, i) => i <= 3 ? GenerateThumbImage(GetImage(x), staticWidth, staticHeight) : null)
                .Where(x => x != null).ToList();
            var nIndex = 0;
            var max_Height = 0;
            var max_Width = 0;
            var counter = 0;
            foreach (var image in images)
            {
                max_Height = Math.Max(max_Height, image.Height);
                if (counter <= 1)
                    max_Width += image.Width;
                counter++;
            }

            var img3 = new Bitmap(max_Width, imageUrl.Count() > 2 ? max_Height * 2 : max_Height,
                PixelFormat.Format32bppArgb);
            var g = Graphics.FromImage(img3);
            g.Clear(Color.Transparent);
            for (var i = 0; i < images.Count(); i++)
            {
                var img = images[i];
                if (i == 0 || nIndex == 0)
                {
                    g.DrawImage(img, new Point(0, i == 0 ? 0 : max_Height));
                    max_Width = img.Width;
                }
                else
                {
                    g.DrawImage(img, new Point(max_Width, i > 2 ? max_Height : 0));
                    max_Width += img.Width;
                }

                if (nIndex >= 1)
                    nIndex = 0;
                else nIndex++;
                img.Dispose();
            }

            g.Dispose();

            using (var memoryStream = new MemoryStream())
            {
                img3.Save(memoryStream, ImageFormat.Png);
                return memoryStream.ToArray();
                //var base64String = Convert.ToBase64String(memoryStream.ToArray());
                //img3.Dispose();
                //return base64String;
            }
        }
    }
}
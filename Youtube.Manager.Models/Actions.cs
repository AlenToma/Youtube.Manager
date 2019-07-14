using System;
using System.IO;
using System.Text.RegularExpressions;
using EntityWorker.Core.Helper;
#if NETCOREAPP2_2
using System.Drawing;
using System.Drawing.Drawing2D;
#endif
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace Youtube.Manager.Models.Container
{
    public static class Actions
    {
        private static string ApplicationRootPath { get; set; } = AppDomain.CurrentDomain.BaseDirectory;

        static Actions()
        {
            if (ApplicationRootPath != null)
                while (ApplicationRootPath.Contains("bin"))
                    ApplicationRootPath = Directory.GetParent(ApplicationRootPath).FullName;
        }

        public static string ImageRootPath => Path.Combine(ApplicationRootPath, "UploadedImages");

        public const string AndroidClientId = "564962228855-c7n1bm28vv6r6rv7gcr68seq4r13cugs";
        public const string SecretKey = "0TLbop3aVDGdw0o0qZjDs_AD";
        public const string YoutubeDeveloperKey = "AIzaSyDxInpTZIjrmhlEz2oSeIw7WX0GXRH0He8";

        public const string SystemYoutubeUserName = "Youtube.Manager";

        public const string SystemYoutubePassword = "Youtube.Manager.Password";


        private static readonly TaskFactory _myTaskFactory = new TaskFactory(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

        public static T Astype<T>(this object item)
        {
            return item.ToType<T>();
        }


        public static T Await<T>(this Task<T> task)
        {
            T result = default;
            if (task == null)
                return result;
            _myTaskFactory.StartNew(new Func<Task>(async () =>
            {
                result = await task; // Simulates a method that returns a task and
                                     // inside it is possible that there
                                     // async keywords or anothers tasks
            })).Unwrap().GetAwaiter().GetResult();
            return result;
        }




        public static T Await<T>(this Func<Task<T>> task)
        {
            return _myTaskFactory.StartNew(task).Unwrap().GetAwaiter().GetResult();
        }



        public static void Await(this Task task)
        {

            task.GetAwaiter().GetResult();

        }

        public static void Await(this Action task)
        {

            _myTaskFactory.StartNew(task).ConfigureAwait(true).GetAwaiter().GetResult();

        }


        static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        public static string SizeSuffix(Int64 value, int decimalPlaces = 1)
        {
            if (decimalPlaces < 0) { throw new ArgumentOutOfRangeException("decimalPlaces"); }
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return string.Format("{0:n" + decimalPlaces + "} bytes", 0); }

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(value, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}",
                adjustedSize,
                SizeSuffixes[mag]);
        }


        public static string GenerateUserFolderName(string email)
        {
            return new Regex(@"[A-Z,a-z].+?(?=\@)").Match(email).Value;
        }

        /// <summary>
        ///     Return embedded file from IProduct.Modules.SQL
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetSQL(string fileName)
        {
            var assembly = typeof(Actions).Assembly;
            var resourceName = $"Youtube.Manager.Models.Container.EntityMigration.SQL.{fileName}";
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                var result = reader.ReadToEnd()?.Trim();
                return result;
            }
        }

        /// <summary>
        /// Format views
        /// </summary>
        /// <param name="value"></param>
        /// <param name="v"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string RoundAndFormat(this long? v, string message = "")
        {
            if (!v.HasValue)
                return string.Empty;
            var value = v.Value;
            var result = string.Empty;
            var negative = value < 0;
            if (negative) value *= -1;

            if (value < 1000)
            {
                result = value.ToString();
            }
            else if (value < 1000000)
            {
                result = RoundDown(value / 1000.0, 0) + "K";
            }
            else if (value < 100000000)
            {
                result = RoundDown(value / 1000000.0, 2) + "M";
            }
            else if (value < 10000000000)
            {
                result = RoundDown(value / 1000000.0, 0) + "M";
            }

            if (negative) return "-" + result + message;
            return result + message;
        }


        public static bool IsList(this Type type)
        {
            return type.GetActualType() != type;
        }

        public static double RoundDown(double value, int digits)
        {
            var pow = Math.Pow(10, digits);
            return Math.Truncate(value * pow) / pow;
        }

#if NETCOREAPP2_2
        public static Bitmap GetImage(string filename)
        {
            using (var client = new System.Net.WebClient())
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
                var target = new Bitmap((int)newSize.Width, (int)newSize.Height);

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
            // download the images from the url and then resize them
            List<Bitmap> images = imageUrl
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

            // the container of the joined images
            var imgContainer = new Bitmap(max_Width, imageUrl.Count() > 2 ? max_Height * 2 : max_Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            var g = Graphics.FromImage(imgContainer);
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
                imgContainer.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                return memoryStream.ToArray();
                //var base64String = Convert.ToBase64String(memoryStream.ToArray());
                //img3.Dispose();
                //return base64String;
            }
        }
#endif

    }
}

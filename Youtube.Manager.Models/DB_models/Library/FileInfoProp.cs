using System;
using System.IO;

namespace Youtube.Manager.Models.Container
{
    public class FileInfoProp
    {
        private readonly string[] Surf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB

        public string FullPath { get => File.FullName; }

        public string Name { get => File.Name; }


        /// <summary>
        ///  "B", "KB", "MB", "GB", "TB", "PB", "EB"
        /// </summary>
        public string Sign
        {
            get
            {
                var byteCount = File.Length;
                if (byteCount == 0)
                    return Surf[0];
                long bytes = Math.Abs(byteCount);
                int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
                return Surf[place];

            }
        }

        public double Size
        {
            get
            {
                var byteCount = File.Length;
                if (byteCount == 0)
                    return 0;
                long bytes = Math.Abs(byteCount);
                int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
                double num = Math.Round(bytes / Math.Pow(1024, place), 1);
                return (Math.Sign(byteCount) * num);

            }
        }

        public void MovtoDirectory(string directoryPath)
        {
            var desPath = Path.Combine(directoryPath, Name);
            File.MoveTo(desPath);
        }

        public readonly FileInfo File;

        public FileInfoProp(FileInfo file)
        {
            File = file;
        }
    }
}

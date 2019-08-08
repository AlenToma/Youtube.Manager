using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Youtube.Manager.Models.Container
{
    public sealed class Logger
    {
        private readonly string LogIdentifier;
        private readonly string LogPath;
        private const string LogFolder = "Logs";

        public string CurrentUserEmail { get; set; }
        public LogLevel LogLevel { get; set; }

        public Logger(string logPath)
        {
            LogPath = Path.Combine(logPath, LogFolder);

            var doc = new DirectoryManager(logPath);

            if (!doc.Exist)
                throw new Exception("LogPath dose not exist");

            doc = doc.Folder(LogFolder).Create();

            var files = doc.GetFiles(SearchOption.TopDirectoryOnly, "Logs_");
            if (files.Count > 10)
            {
                while (files.Count > 1 || files.Any(x => new string[4] { "GB", "TB", "PB", "EB" }.ToList().Contains(x.Sign)))
                {
                    files.First().File.Delete();
                    files.RemoveAt(0);
                }
            }

            var file = files.OrderBy(x => x.Size).FirstOrDefault();
            if (file != null && (file.Size <= 1 || !new string[4] { "GB", "TB", "PB", "EB" }.ToList().Contains(file.Sign)))
            {
                LogIdentifier = file.FullPath;
            }
            else
            {
                LogIdentifier = files.Any() ? $"Log_{Convert.ToInt32(files.Select(x => x.Name.Substring(x.Name.IndexOf('_'), x.Name.IndexOf("."))))}.txt" : "Log_1.txt";
            }

            LogIdentifier = Path.Combine(LogPath, LogIdentifier);

            File.Open(LogIdentifier, FileMode.OpenOrCreate).Close();

        }


        private void UploadLog(string errorMessage)
        {
            var email = !string.IsNullOrWhiteSpace(CurrentUserEmail) ? CurrentUserEmail : Actions.SystemYoutubeUserName;
            errorMessage = Convert.ToBase64String(Encoding.UTF8.GetBytes(errorMessage));
            ControllerRepository.Db(x => x.AddLog(email, errorMessage));
        }

        private void LogError(string message, LogLevel logLevel)
        {
            lock (this)
            {
                if (logLevel == LogLevel)
                    using (StreamWriter stream = new StreamWriter(LogIdentifier, append: true))
                        stream.WriteLine($"Error: {DateTime.Now}: {message}");

                UploadLog(message);
            }
        }

        public List<string> GetLog()
        {
            return File.ReadLines(LogIdentifier, Encoding.UTF8).ToList();
        }

        public void Error(string message, LogLevel logLevel)
        {
            LogError(message, logLevel);
        }

        public void Error(Exception exception)
        {
            LogError(exception.Message, LogLevel);

        }
        public void Error(object exception)
        {
            LogError(exception != null ? JsonConvert.SerializeObject(exception) : "", LogLevel);
        }

        public void Clear()
        {
            lock (this)
                File.WriteAllText(LogIdentifier, string.Empty);
        }


        public void Info(string message, object infoData = null, LogLevel logLevel = LogLevel.Debug)
        {

            if (logLevel == LogLevel)
            {
                lock (this)
                {
                    using (StreamWriter stream = new StreamWriter(LogIdentifier, append: true))
                        stream.WriteLine($"{DateTime.Now}: {message} - {infoData ?? ""}");
                }
            }
        }
    }
}

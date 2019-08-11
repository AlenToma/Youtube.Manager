using System;
using System.IO;
using System.Net;

namespace Realm.Of.Y.Manager.Models.Container
{
    public class DownlodFile : IDisposable
    {
        private readonly WebClient _client;
        private readonly Action<int> _onProgress;
        private readonly Action<Exception> _onError;
        private readonly Action _onComplete;
        private readonly Uri _url;
        private readonly string _fileName;
        private readonly string _directoryName;

        public bool Downloading { get; private set; }
        // this will be true when the complete event trigger or an error trigger
        public bool FileCompleted { get; private set; }


        private readonly Logger Logger;

        /// <summary>
        /// DownlodFile
        /// </summary>
        /// <param name="url">File Url</param>
        /// <param name="filelocation">Where file will e saved on the system</param>
        /// <param name="progressChanged">Get the ProgressPercentage</param>
        /// <param name="error">When an error accord</param>
        public DownlodFile(Logger logger, string url, string directoryName, string fileName, Action<int> progressChanged = null, Action completed = null, Action<Exception> error = null)
        {
            Logger = logger;
            _client = new WebClient();
            _onProgress = progressChanged;
            _fileName = fileName;
            _directoryName = directoryName;
            _onComplete = completed;
            _onError = error;
            _url = url.StartsWith("http", StringComparison.OrdinalIgnoreCase) ? new Uri(url) : new Uri("http://" + url);




            var proc = 0;
            _client.DownloadProgressChanged += new DownloadProgressChangedEventHandler((o, e) =>
            {
                if (proc != e.ProgressPercentage)
                    _onProgress?.Invoke(e.ProgressPercentage);
                proc = e.ProgressPercentage;

            });
            _client.DownloadDataCompleted += new DownloadDataCompletedEventHandler((o, e) =>
            {
                try
                {

                    if (e.Cancelled)
                    {
                        Logger?.Info("Downloading Cancelled");
                        return;
                    }
                    else if (e.Error != null)
                    {
                        Logger?.Error(e.Error);
                        return;
                    }
                    else
                    {
                        if (!Directory.Exists(_directoryName))
                            Directory.CreateDirectory(_directoryName);

                        File.WriteAllBytes(Path.Combine(_directoryName, _fileName), e.Result);
                        _onComplete?.Invoke();
                    }
                    this.Dispose();
                }
                catch
                {
                    this.Dispose();
                }

            });




        }


        public void Download()
        {
            try
            {




                if (!FileCompleted)
                {
                    Logger?.Info("Downloading", _url);
                    Downloading = true;
                    // Start downloading the file
                    _client.DownloadDataAsync(_url);
                }
                else throw new Exception("DownlodFile is already disposed, please Create a new one");
            }
            catch (Exception ex)
            {
                FileCompleted = true;
                this.Dispose();
                Logger?.Error(ex);
                _onError?.Invoke(ex);
            }

        }


        public void Dispose()
        {
            if (!FileCompleted)
                _client.Dispose();
            FileCompleted = true;
            Downloading = false;
        }
    }
}

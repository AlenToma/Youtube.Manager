using System.Collections.Generic;
using Realm.Of.Y.Manager.Models.Container;
using Realm.Of.Y.Manager.Models.Container.DB_models;
using Realm.Of.Y.Manager.Models.Container.Interface;

namespace Realm.Of.Y.Manager.Helper
{
    public static class ObjectCacher
    {
        /// <summary>
        ///     Cached Image are stored Here
        /// </summary>
        public static readonly Dictionary<string, byte[]> CachedImages = new Dictionary<string, byte[]>();


        /// <summary>
        ///     Files to download <videoId, YoutubeFileDownloadItem />
        /// </summary>
        public static readonly Dictionary<string, YoutubeFileDownloadItem> DownloadingFiles =
            new Dictionary<string, YoutubeFileDownloadItem>();


        /// <summary>
        ///     those will be triggered when an operation to the api has been called
        /// </summary>
        public static Dictionary<ModuleTrigger, List<string>> ModuleTriggerCacher =
            new Dictionary<ModuleTrigger, List<string>>();

        /// <summary>
        ///     Stored RandomID
        /// </summary>
        public static Dictionary<int, int> RandomIdCacher = new Dictionary<int, int>();


        public static void Clear()
        {
            CachedImages.Clear();
            DownloadingFiles.Clear();
            ModuleTriggerCacher.Clear();
            RandomIdCacher.Clear();
        }
    }
}
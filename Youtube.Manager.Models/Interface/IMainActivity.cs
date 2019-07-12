using System.Threading.Tasks;

namespace Youtube.Manager.Models.Container.Interface
{
    public interface IMainActivity
    {
        /// <summary>
        /// Activate Google login
        /// </summary>
        void OnLoginClick();

        /// <summary>
        /// Download Video
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        void DownloadVideo(YoutubeFileDownloadItem video);

        /// <summary>
        /// Validate network Activity
        /// </summary>
        /// <returns></returns>
        Task<bool> IsOnline();

        /// <summary>
        /// Validate if the app has the required permission Storage
        /// </summary>
        /// <returns></returns>
        Task<bool> ValidateStoragePermission();


        /// <summary>
        ///  The location of the music folder
        /// </summary>
        string LocalMusicFolder { get; }

        /// <summary>
        /// Logs Error 
        /// </summary>
        Logger Logger { get; }

        /// <summary>
        /// Get the saved user local settings
        /// </summary>
        LocalFileSettings UserLocalSettings { get; }

        /// <summary>
        /// Request new VideoAdd
        /// </summary>
        void ReguastNewAdd();
    }
}

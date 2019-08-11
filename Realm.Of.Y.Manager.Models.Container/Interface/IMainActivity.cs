using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Realm.Of.Y.Manager.Models.Container.DB_models;
using Realm.Of.Y.Manager.Models.Container.DB_models.Library;

namespace Realm.Of.Y.Manager.Models.Container.Interface
{
    public interface IMainActivity
    {
        string BannerAdd { get; set; }

        string RewardAddId { get; set; }

        string AdsApplicationIds { get; set; }

        string YDeveloperKey { get; set; }

        /// <summary>
        /// when the login is completed
        /// </summary>
        Action<User, string> OnLoginComplete { get; set; }

        /// <summary>
        /// Activate Google login
        /// </summary>
        void OnLoginClick();

        /// <summary>
        /// Download Video
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        void DownloadVideo(YFileDownloadItem video);

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

        /// <summary>
        /// the service is avilable
        /// </summary>
        /// <returns></returns>
        Task<bool> CanPurchase();

        /// <summary>
        /// Buy the item
        /// </summary>
        /// <param name="appBillingProduct"></param>
        /// <returns></returns>
        Task<bool> Buy(AppBillingProduct appBillingProduct);

        /// <summary>
        /// Get products to buy
        /// </summary>
        /// <returns></returns>
        Task<List<AppBillingProduct>> GetProducts();
    }
}

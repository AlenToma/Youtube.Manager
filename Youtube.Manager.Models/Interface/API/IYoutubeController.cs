using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Youtube.Manager.Models.Container.Attributes;
using Youtube.Manager.Models.Container.DB_models.Library;

namespace Youtube.Manager.Models.Container.Interface.API
{
    [Route(url: "api/")]
    public interface IYoutubeController
    {

        // we may need to add some changes here later as if now, the Google provider
        // take care of the login

        object Google(object error = null);

        /// <summary>
        /// Get a collection of the searched youtube videos
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="pageSize"></param>
        /// <param name="relatedTo"></param>
        /// <param name="videoSearchType"></param>
        /// <returns></returns>
        [Route]
        Task<YoutubeVideoCollection> SearchAsync(long userId, string searchString, int pageSize, int pageNumber, string relatedTo = null, VideoSearchType videoSearchType = VideoSearchType.Videos);
        /// <summary>
        /// Get the playlist video contents
        /// </summary>
        /// <param name="playListId"></param>
        /// <returns></returns>
        [Route]
        Task<IEnumerable<VideoWrapper>> GetPlaylistVideosAsync(string playlistId, int pageNumber, int pageSize);

        [Route]
        Task<IEnumerable<VideoWrapper>> GetChannelVideosAsync(string channelId, int pageNumber, int pageSize);

        /// <summary>
        /// decrypted youtube video
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        [Route]
        Task<List<YoutubeVideoInfo>> GetVideoAsync(string videoId, int? formatCode = 18);

        /// <summary>
        /// Stream youtube video
        /// </summary>
        /// <param name="id"></param>
        /// <param name="formatCode"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> StreamVideo(string id, int? formatCode = null);

        /// <summary>
        /// Return and image
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        object GetImage(string imagePath);

        /// <summary>
        /// Get Suggesting from google
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [Route]
        Task<IEnumerable<string>> GetSuggestQueries(string text);
    }
}

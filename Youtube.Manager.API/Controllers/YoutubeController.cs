using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Youtube.Manager.API.Controllers.BaseController;
using Youtube.Manager.API.Models;
using Youtube.Manager.Core;
using Youtube.Manager.Models.Container;

namespace Youtube.Manager.API.Controllers
{
    public class YoutubeController : AdminAPController
    {
        // GET api/values/5
        [HttpGet]
        public async Task<JsonNetResult> Get(string searchString, int pageSize = 50, string relatedTo = null, VideoSearchType videoSearchType = VideoSearchType.Videos)
        {
            var result = new YoutubeVideoCollection();
            using (var m = new YManager())
            {
                var videoResult = $"{searchString}, -\"Playlist\"";

                var albumResult = $"{searchString}, +\"Albums\", \"Playlist\"";

                var playListResult = $"{searchString}, +\"Playlist\"";
                if (videoSearchType == VideoSearchType.Videos || videoSearchType == VideoSearchType.All)
                    result.Videos = m.Search(videoResult, relatedTo,
                        string.IsNullOrEmpty(relatedTo) ? VideoSearchType.Videos : VideoSearchType.All, pageSize);

                if (string.IsNullOrEmpty(relatedTo))
                {
                    if (videoSearchType == VideoSearchType.Album || videoSearchType == VideoSearchType.All)
                        result.Albums = m.Search(albumResult, relatedTo, VideoSearchType.Album, pageSize);

                    if (videoSearchType == VideoSearchType.PlayList || videoSearchType == VideoSearchType.All)
                        result.Playlists = m.Search(playListResult, relatedTo, VideoSearchType.PlayList, pageSize);
                }
            }

            return await result.JsonResultAsync(Request);
        }


        [HttpGet]
        public async Task<JsonNetResult> GetPlaylistItems(string id)
        {
            using (var m = new YManager())
            {
                return await m.GetPlaylist(id).JsonResultAsync(Request);
            }
        }

        /// <summary>
        ///     Prepare videos and generate its downloadUrl
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonNetResult> GetVideos(string id)
        {
            using (var m = new YManager())
            {
                return await (await m.GetVideos(id)).JsonResultAsync(Request);
            }
        }


        /// <summary>
        ///     If video is downloadable
        /// </summary>
        /// <param name="id"></param>
        /// <param name="formatCode"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> StreamHandShake(string id, int? formatCode = null)
        {
            using (var m = new YManager())
            {
                var video = await m.GetVideos(id, formatCode);
                return video?.Url;
            }
        }


        /// <summary>
        ///     Stream the file
        /// </summary>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <param name="formatCode"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        public async Task<HttpResponseMessage> StreamVideo(string id, int? formatCode = null)
        {
            using (var m = new YManager())
            {
                var video = await m.GetVideos(id, formatCode);
                var bytes = video?.Data;
                var v = new VideoStream(bytes);
                var response = Request.CreateResponse();
                response.Content = new PushStreamContent(v.WriteToStream, new MediaTypeHeaderValue("video/mp4"));
                if (bytes != null) response.Content.Headers.ContentLength = bytes.Length;
                return response;
            }
        }
    }
}
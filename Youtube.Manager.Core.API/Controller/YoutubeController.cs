using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Youtube.Manager.Models.Container;
using Youtube.Manager.Models.Container.DB_models;
using Youtube.Manager.Models.Container.DB_models.Library;
using Youtube.Manager.Models.Container.Interface.API;

namespace Youtube.Manager.Core.API.Controller
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class YoutubeController : ControllerBase, IYoutubeController
    {

        [HttpPost]
        public async void SyncData()
        {
            using (var db = new DbRepository())
            {
                var videoData = db.Get<VideoData>().Where(x => string.IsNullOrEmpty(x.Duration)).Execute();
                foreach (var data in videoData)
                {
                    var video = (await this.GetVideoAsync(data.Video_Id)).FirstOrDefault();
                    if (video != null)
                    {
                        data.Auther = video.Auther;
                        data.Duration = video.Duration;
                        data.Description = video.Description;
                        data.Quality = video.Quality;
                        data.Resolution = video.Resolution;
                        db.Save(data);
                    }
                }
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<List<YoutubeVideoInfo>> GetVideoAsync(string videoId, int? formatCode = 18)
        {
            if (videoId.Contains("youtube", System.StringComparison.OrdinalIgnoreCase))
                videoId = videoId.Split('=').Last();

            return await new YManager().GetVideos(videoId, formatCode);

        }

        [HttpGet]
        public async Task<IEnumerable<VideoWrapper>> GetPlaylistVideosAsync(string playlistId, int pageNumber, int pageSize)
        {
            return await new YManager().GetPlaylistVideosAsync(playlistId);
        }

        [HttpGet]
        public async Task<IEnumerable<VideoWrapper>> GetChannelVideosAsync(string channelId, int pageNumber, int pageSize)
        {
            return await new YManager().GetChannelVideosAsync(channelId);
        }

        [HttpGet]
        public async Task<IEnumerable<string>> GetSuggestQueries(string text)
        {
            return await new YManager().GetSuggestQueries(text);
        }

        [HttpGet]
        public async Task<YoutubeVideoCollection> SearchAsync(long userId, string searchString, int pageSize, int pageNumber, string relatedTo = null, VideoSearchType videoSearchType = VideoSearchType.Videos)
        {
            var result = new YoutubeVideoCollection();
            using (var db = new DbRepository())
            {
                var m = new YManager();
                var videoResult = $"{searchString}, -\"Playlist\"";

                var albumResult = $"{searchString}, +\"Albums\", \"Playlist\"";

                var playListResult = $"{searchString}, +\"Playlist\"";
                if (videoSearchType == VideoSearchType.Videos || videoSearchType == VideoSearchType.All || videoSearchType == VideoSearchType.Mix)
                    result.Videos.AddRange(await m.SearchAsync(videoResult, relatedTo, videoSearchType != VideoSearchType.Mix ? VideoSearchType.Videos : videoSearchType, pageNumber, pageSize));

                if (videoSearchType == VideoSearchType.Recommendation)
                {
                    var search = new List<string>();
                    var userSearchedItem = db.Get<UserSearch>().Where(x => x.User_Id == userId).OrderByDescending(x => x.Counter).Take(1).ExecuteFirstOrDefault();
                    if (userSearchedItem != null)
                        search.Add(userSearchedItem.Text);
                    userSearchedItem = db.Get<UserSearch>().Where(x => x.User_Id != userId).OrderByDescending(x => x.Counter).Take(userSearchedItem == null ? 2 : 1).ExecuteFirstOrDefault();
                    if (userSearchedItem != null)
                        search.Add(userSearchedItem.Text);

                    result.Videos.AddRange(await m.SearchAsync($"{string.Join("|", search)} , +\"Albums\", \"Playlist\"", relatedTo, videoSearchType, pageNumber, pageSize));

                }

                if (string.IsNullOrEmpty(relatedTo))
                {
                    if (videoSearchType == VideoSearchType.Album || videoSearchType == VideoSearchType.All)
                        result.Albums.AddRange(await m.SearchAsync(albumResult, relatedTo, VideoSearchType.Album, pageNumber, pageSize));

                    if (videoSearchType == VideoSearchType.PlayList || videoSearchType == VideoSearchType.All)
                        result.Playlists.AddRange(await m.SearchAsync(playListResult, relatedTo, VideoSearchType.PlayList, pageNumber, pageSize));
                }

                if (!string.IsNullOrEmpty(searchString) && result.Videos.Any() && videoSearchType != VideoSearchType.Recommendation)
                {
                    var item = db.Get<UserSearch>().Where(x => x.Text.Contains(searchString) && x.User_Id == userId).ExecuteFirstOrDefault() ?? new UserSearch() { Text = searchString, User_Id = userId };
                    item.Counter = (item.Counter ?? 0) + 1;
                    db.Save(item).SaveChanges();
                }
            }
            return result;
        }

        /// <summary>
        /// Stream the file
        /// </summary>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <param name="formatCode"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        public async Task<HttpResponseMessage> StreamVideo(string id, int? formatCode = null)
        {

            var video = await new YManager().GetVideos(id, formatCode);
            //var bytes = video?.Data;
            //var v = new VideoStream(bytes);
            var response = new HttpResponseMessage();
            //response.Content = new PushStreamContent(v.WriteToStream, new MediaTypeHeaderValue("video/mp4"));
            //if (bytes != null) response.Content.Headers.ContentLength = bytes.Length;
            return response;

        }

        [HttpGet]
        [AllowAnonymous]
        public object GetImage(string imagePath)
        {
            var path = Path.Combine(Actions.ImageRootPath, imagePath);
            if (!System.IO.File.Exists(path))
                return new EmptyResult();

            //var bytes = System.IO.File.OpenRead(path);
            return new FileContentResult(System.IO.File.ReadAllBytes(path), "image/png");
            //return File(bytes, "image/png");
        }


        // we may need to add some changes here later as if now, the Google provider
        // take care of the login
        [AllowAnonymous]
        public object Google(object error = null)
        {
            //var test = Request.IsAuthenticated;

            return new EmptyResult();
        }
    }
}

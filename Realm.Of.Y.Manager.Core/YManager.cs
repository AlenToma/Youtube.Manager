using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityWorker.Core.Helper;
using Realm.Of.Y.Manager.Models.Container;
using Realm.Of.Y.Manager.Models.Container.DB_models.Library;
using YoutubeExplode;
using YoutubeExplode.Models;

namespace Realm.Of.Y.Manager.Core
{
    public class YManager
    {

        private VideoWrapper Create(Video video)
        {
            return new VideoWrapper()
            {
                VideoType = video.VideoType.ToString(),
                Author = video.Author,
                UploadDate = video.UploadDate,
                Title = video.Title,
                DefaultThumbnailUrl = video.Thumbnails.HighResUrl,
                Duration = video.Duration.TotalHours >= 1 ? video.Duration.ToString("c") : string.Format("{0:mm}:{1:ss}", video.Duration, video.Duration),
                Views = video.Views,
                TotalVideoViews = video.TotalVideoViews,
                Description = video.Description,
                Id = video.Id,
                IsPlaylist = video.VideoType == YoutubeExplode.Models.VideoType.Playlist,
                IsChannel = video.VideoType == YoutubeExplode.Models.VideoType.Channel,
                IsVideo = video.VideoType == YoutubeExplode.Models.VideoType.Video,
            };
        }

        private readonly YoutubeClient dataContext = new YoutubeClient();

        /// <summary>
        /// Get the dycrptet videos url
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<YVideoInfo>> GetVideos(string id, int? formatCode = null)
        {
            try
            {
                var result = new List<YVideoInfo>();
                var streamInfoSet = await dataContext.GetVideoMediaStreamInfosAsync(id);
                var video = await dataContext.GetVideoAsync(id);

                foreach (var item in streamInfoSet?.Muxed)
                {
                    if (formatCode.HasValue && formatCode.Value != item.Itag)
                        continue;
                    result.Add(new YVideoInfo()
                    {
                        FormatCode = item.Itag,
                        Url = item.Url,
                        Quality = item.VideoQualityLabel,
                        Resolution = item.Resolution.ToString(),
                        Size = Actions.SizeSuffix(item.Size),
                        Duration = video != null ? video.Duration.TotalHours >= 1 ? video.Duration.ToString("c") : string.Format("{0:mm}:{1:ss}", video.Duration, video.Duration) : "",
                        Description = video?.Description,
                        Auther = video?.Author
                    });

                }
                return result;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get Youtube Video Item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Video> YoutubeVideos(string id)
        {
            return await (dataContext.GetVideoAsync(id));
        }

        /// <summary>
        /// Get the playList videos 
        /// </summary>
        /// <param name="playlistId">comma seperated ids</param>
        /// <returns></returns>
        public async Task<IEnumerable<VideoWrapper>> GetPlaylistVideosAsync(string playlistId, int pageNumber = 1, int pageSize = 30)
        {
            return (await dataContext.GetPlaylistAsync(playlistId, pageNumber, pageSize))?.Videos?.Select(x => Create(x));
        }

        /// <summary>
        /// Get the playList videos 
        /// </summary>
        /// <param name="playlistId">comma seperated ids</param>
        /// <returns></returns>
        public async Task<IEnumerable<VideoWrapper>> GetChannelVideosAsync(string channelId, int pageNumber = 1, int pageSize = 30)
        {
            return (await dataContext.GetChannelUploadsAsync(channelId, pageNumber, pageSize))?.Select(x => Create(x));
        }


        /// <summary>
        /// Get Suggesting
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetSuggestQueries(string text)
        {
            return await dataContext.GetSuggestQueries(text);
        }

        /// <summary>
        /// Search Youtube
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VideoWrapper>> SearchAsync(string searchText, string relatedTo = null, VideoSearchType videoSearchType = VideoSearchType.Videos, int pageNumber = 1, int pageSize = 20)
        {
            if (!string.IsNullOrEmpty(relatedTo))
            {
                searchText = $"(\"{searchText}\" | {string.Join("\"{0}\"|", relatedTo.Split(','))})";
            }

            SearchFilterType searchFilterType = SearchFilterType.Default;

            switch (videoSearchType)
            {
                case VideoSearchType.Videos:
                    searchFilterType = SearchFilterType.Video;
                    break;
                case VideoSearchType.Album:
                case VideoSearchType.PlayList:
                case VideoSearchType.Recommendation:
                    searchFilterType = SearchFilterType.Playlist;
                    break;
                case VideoSearchType.Mix:
                case VideoSearchType.All:
                    searchFilterType = SearchFilterType.Default;
                    break;
                case VideoSearchType.Channel:
                    searchFilterType = SearchFilterType.Channel;
                    break;
                case VideoSearchType.Rating:
                    searchFilterType = SearchFilterType.Rating;
                    break;
            }

            var data = await dataContext.SearchVideosByFilterAsync(searchText, pageNumber, pageSize, searchFilterType);
            return (string.IsNullOrEmpty(searchText) ? data.OrderByDescending(x => x.Statistics.ViewCount) : data.OrderBy(x => x.Title.Contains(searchText)).ThenByDescending(x => x.Statistics.ViewCount)).Select(x => Create(x));


        }
    }
}

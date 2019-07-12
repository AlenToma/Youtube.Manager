using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using YoutubeExplode.Models;

namespace Youtube.Manager.Models.Container.DB_models.Library
{
    public partial class VideoWrapper
    {

        private List<VideoWrapper> _videos;

        [JsonIgnore]
        public List<VideoWrapper> Videos
        {
            get
            {
                if (IsVideo || _videos != null)
                    return _videos;
                //throw new Exception("Videos Need to be loaded.");
                return null;
            }
            set
            {
                _videos = value;
            }
        }

        public VideoWrapper(Video video)
        {
            VideoType = video.VideoType.ToString();
            Author = video.Author;
            UploadDate = video.UploadDate;
            Title = video.Title;
            DefaultThumbnailUrl = video.Thumbnails.HighResUrl;
            Duration = video.Duration.TotalHours >= 1 ? video.Duration.ToString("c") : string.Format("{0:mm}:{1:ss}", video.Duration, video.Duration);
            Views = video.Views;
            TotalVideoViews = video.TotalVideoViews;
            Description = video.Description;
            Id = video.Id;
            IsPlaylist = video.VideoType == YoutubeExplode.Models.VideoType.Playlist;
            IsChannel = video.VideoType == YoutubeExplode.Models.VideoType.Channel;
            IsVideo = video.VideoType == YoutubeExplode.Models.VideoType.Video;
        }

        public VideoWrapper(VideoData video)
        {
            VideoType = YoutubeExplode.Models.VideoType.Video.ToString();
            Author = video.Auther;
            Title = video.Title;
            DefaultThumbnailUrl = video.ThumpUrl;
            Duration = video.Duration;
            Views = "";
            TotalVideoViews = "";
            Description = video.Description;
            Id = video.Video_Id;
            IsPlaylist = false;
            IsChannel = false;
            IsVideo = true;
        }

        [JsonConstructor]
        public VideoWrapper() { }

        public string Duration { get; set; }

        public DateTimeOffset? UploadDate { get; set; }

        public string VideoType { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public string DefaultThumbnailUrl { get; set; }

        /// <summary>
        /// View as string
        /// </summary>
        public string Views { get; set; }
        /// <summary>
        /// TotalVideoViews as string
        /// </summary>
        public string TotalVideoViews { get; set; }

        public bool IsPlaylist { get; set; }

        public bool IsChannel { get; set; }

        public bool IsVideo { get; set; }

        public string Id { get; set; }

        public bool HasVideos { get => _videos != null && _videos.Any(); }


        /// <summary>
        /// Depneding on the videoType this will load videos from Channel or playlist
        /// </summary>
        /// <returns></returns>
        public VideoWrapper LoadVideos()
        {
            if (_videos != null || IsVideo)
                return this;

            if (IsPlaylist)
                _videos = ControllerRepository.Youtube(x => x.GetPlaylistVideosAsync(Id, 1, 30)).Await().ToList();
            else if (IsChannel)
                _videos = ControllerRepository.Youtube(x => x.GetChannelVideosAsync(Id, 1, 30)).Await().ToList();
            return this;
        }

    }
}

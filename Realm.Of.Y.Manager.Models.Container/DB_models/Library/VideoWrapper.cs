using Newtonsoft.Json;
using Rest.API.Translator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Realm.Of.Y.Manager.Models.Container.DB_models.Library
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

  

        public VideoWrapper(VideoData video)
        {
            VideoType = "Video";
            Author = video.Auther;
            Title = video.Title;
            DefaultThumbnailUrl = video.ThumpUrl;
            Duration = video.Duration ?? "";
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
                _videos = ControllerRepository.Y(x => x.GetPlaylistVideosAsync(Id, 1, 50)).Await().ToList();
            else if (IsChannel)
                _videos = ControllerRepository.Y(x => x.GetChannelVideosAsync(Id, 1, 50)).Await().ToList();
            return this;
        }

    }
}

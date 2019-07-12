using System.Collections.Generic;
using Youtube.Manager.Models.Container.DB_models.Library;

namespace Youtube.Manager.Models.Container
{
    public class YoutubeVideoCollection 
    {
        public List<VideoWrapper> Videos { get; set; } = new List<VideoWrapper>();

        public List<VideoWrapper> Albums { get; set; } = new List<VideoWrapper>();

        public List<VideoWrapper> Playlists { get; set; } = new List<VideoWrapper>();

        public List<VideoWrapper> Channels { get; set; } = new List<VideoWrapper>();
    }
}

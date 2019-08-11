using System.Collections.Generic;
using Realm.Of.Y.Manager.Models.Container.DB_models.Library;

namespace Realm.Of.Y.Manager.Models.Container
{
    public class YVideoCollection 
    {
        public List<VideoWrapper> Videos { get; set; } = new List<VideoWrapper>();

        public List<VideoWrapper> Albums { get; set; } = new List<VideoWrapper>();

        public List<VideoWrapper> Playlists { get; set; } = new List<VideoWrapper>();

        public List<VideoWrapper> Channels { get; set; } = new List<VideoWrapper>();
    }
}

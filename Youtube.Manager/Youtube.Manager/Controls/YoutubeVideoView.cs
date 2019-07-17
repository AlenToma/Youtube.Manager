using System;
using Xamarin.Forms;
using Youtube.Manager.Models.Container;
using Youtube.Manager.Models.Container.Interface;

namespace Youtube.Manager.Controls
{
    public class YoutubeVideoView : View, IPlayer
    {
        public MediaItem[] VideoSource { get; private set; }
        public Action<Exception> OnError { get; set; }
        public Action<bool> OnFullScrean { get; set; }
        public Action OnNext { get; set; }
        public Action<MediaItem[]> OnPlayVideo { get; set; }
        public Action OnPrev { get; set; }
        public Action OnVideoEnded { get; set; }
        public Action<MediaItem> OnVideoStarted { get; set; }
        public Action<bool> SetFullScrean { get; set; }
        public PlayerState State { get; set; }
        public Func<MediaItem> GetCurrentMedia { get; set; }

        public Action Stop { get; set; }

        public Action Play { get; set; }

        public Action Abort { get; set; }

        public Action Reset { get; set; }

        public Action<int> PlayQueueItem { get; set; }

        public void PlayVideos(params MediaItem[] video)
        {
            if (video != null)
                VideoSource = video;
            OnPlayVideo?.Invoke(video);
        }
    }
}
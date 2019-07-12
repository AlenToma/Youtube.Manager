using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Youtube.Manager.Helper;
using Youtube.Manager.Models.Container;
using Youtube.Manager.Models.Container.Interface;

namespace Youtube.Manager.Controls
{
    public class MixVideoPlayer : StackLayout
    {
        public List<MediaItem> VideoSource = new List<MediaItem>();

        private YoutubeVideoView youtubePlayer = new YoutubeVideoView();

        private LocalVideoView videoView = new LocalVideoView();

        public IPlayer CurrentPlayer { get; private set; }

        public Action<Exception> OnError
        {
            get => CurrentPlayer.OnError;
            set
            {
                videoView.OnError = value;
                youtubePlayer.OnError = value;
            }
        }

        public Action<bool> OnFullScrean
        {
            get => CurrentPlayer.OnFullScrean;
            set
            {
                videoView.OnFullScrean = value;
                youtubePlayer.OnFullScrean = value;
            }
        }

        public Action OnNext
        {
            get => CurrentPlayer.OnNext;
            set
            {
                videoView.OnNext = value;
                youtubePlayer.OnNext = value;
            }
        }

        public Action<MediaItem[]> OnPlayVideo
        {
            get => CurrentPlayer.OnPlayVideo;
            set
            {
                videoView.OnPlayVideo = value;
                youtubePlayer.OnPlayVideo = value;
            }
        }

        public Action OnPrev
        {
            get => CurrentPlayer.OnPrev;
            set
            {
                videoView.OnPrev = value;
                youtubePlayer.OnPrev = value;
            }
        }

        public Action OnVideoEnded
        {
            get => CurrentPlayer.OnPrev;
            set
            {
                videoView.OnVideoEnded = value;
                youtubePlayer.OnVideoEnded = value;
            }
        }

        public Action<MediaItem> OnVideoStarted
        {
            get => CurrentPlayer.OnVideoStarted;
            set
            {
                videoView.OnVideoStarted = value;
                youtubePlayer.OnVideoStarted = value;
            }
        }

        public Action<bool> SetFullScrean
        {
            get => CurrentPlayer.SetFullScrean;
            set
            {
                videoView.SetFullScrean = value;
                youtubePlayer.SetFullScrean = value;
            }
        }

        public PlayerType PlayerType { get; set; }

        public PlayerState State => CurrentPlayer?.State ?? PlayerState.Stopped;

        public MediaItem CurrentMedia => CurrentPlayer?.GetCurrentMedia?.Invoke() ?? null;

        public void PlayVideos(params MediaItem[] videos)
        {
            CurrentPlayer?.Reset();
            if (videos != null)
                VideoSource = videos.ToList();
            OnPlayVideo?.Invoke(videos);
        }

        public void PlayVideo(MediaItem video)
        {
            CurrentPlayer?.Reset();
            var index = -1;
            if (video != null && (VideoSource?.Any(x => x.Url == video.Url) ?? false))
            {
                index = VideoSource.FindIndex(x => x.Url == video.Url);
                VideoSource.RemoveAll(x => x.Url == video.Url);
            }

            var newList = new List<MediaItem>();
            for (var i = 0; i < VideoSource.Count; i++)
            {
                if (i > index)
                    newList.Add(VideoSource[i]);
            }

            if (index == 0)
                index = 0;
            newList.Insert(index, video);
            VideoSource = newList;
            OnPlayVideo?.Invoke(VideoSource.ToArray());
        }


        public MixVideoPlayer()
        {
            youtubePlayer.IsVisible = videoView.IsVisible = false;
            youtubePlayer.VerticalOptions = videoView.VerticalOptions = LayoutOptions.FillAndExpand;
            youtubePlayer.HorizontalOptions = videoView.HorizontalOptions = LayoutOptions.FillAndExpand;
            this.Children.Add(youtubePlayer);
            this.Children.Add(videoView);
            Methods.ApplicationPlayer = this;
        }

        protected override void OnRemoved(View view)
        {
            Methods.ApplicationPlayer = null;
            base.OnRemoved(view);
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            SwitchPlayer(PlayerType);
        }

        public void SwitchPlayer(PlayerType playerType)
        {
            if (playerType == PlayerType && CurrentPlayer != null)
                return;
            var current = CurrentMedia;
            CurrentPlayer?.Stop();
            CurrentPlayer?.Abort();
            PlayerType = playerType;
            if (playerType == PlayerType.Youtube)
            {
                CurrentPlayer = youtubePlayer;
                youtubePlayer.IsVisible = true;
                videoView.IsVisible = false;
            }
            else
            {
                CurrentPlayer = videoView;
                youtubePlayer.IsVisible = false;
                videoView.IsVisible = true;
            }

            if (current != null)
                PlayVideo(current);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Realm.Of.Y.Manager.Helper;
using Realm.Of.Y.Manager.Models.Container;
using Realm.Of.Y.Manager.Models.Container.Interface;

namespace Realm.Of.Y.Manager.Controls
{
    public class MixVideoPlayer : RelativeLayout
    {
        public List<MediaItem> VideoSource = new List<MediaItem>();

        private readonly YVideoView yPlayer = new YVideoView();

        private readonly LocalVideoView videoView = new LocalVideoView();

        public IPlayer Player { get; private set; }

        public Action<Exception> OnError
        {
            get => Player.OnError;
            set
            {
                videoView.OnError = value;
                yPlayer.OnError = value;
            }
        }

        public Action<bool> OnFullScrean
        {
            get => Player.OnFullScrean;
            set
            {
                videoView.OnFullScrean = value;
                yPlayer.OnFullScrean = value;
            }
        }

        public Action OnNext
        {
            get => Player.OnNext;
            set
            {
                videoView.OnNext = value;
                yPlayer.OnNext = value;
            }
        }

        public Action<MediaItem[]> OnPlayVideo
        {
            get => Player.OnPlayVideo;
            set
            {
                videoView.OnPlayVideo = value;
                yPlayer.OnPlayVideo = value;
            }
        }

        public Action OnPrev
        {
            get => Player.OnPrev;
            set
            {
                videoView.OnPrev = value;
                yPlayer.OnPrev = value;
            }
        }

        public Action OnVideoEnded
        {
            get => Player.OnPrev;
            set
            {
                videoView.OnVideoEnded = value;
                yPlayer.OnVideoEnded = value;
            }
        }

        public Action<MediaItem> OnVideoStarted
        {
            get => Player.OnVideoStarted;
            set
            {
                videoView.OnVideoStarted = value;
                yPlayer.OnVideoStarted = value;
            }
        }

        public Action<bool> SetFullScrean
        {
            get => Player.SetFullScrean;
            set
            {
                videoView.SetFullScrean = value;
                yPlayer.SetFullScrean = value;
            }
        }


        public PlayerType PlayerType { get; set; }

        public PlayerState State => Player?.State ?? PlayerState.Stopped;

        public MediaItem CurrentMedia => Player?.GetCurrentMedia?.Invoke() ?? null;

        public void PlayVideos(params MediaItem[] videos)
        {
            Player?.Reset();
            if (videos != null)
                VideoSource = videos.ToList();
            OnPlayVideo?.Invoke(videos);
        }


        public void PlayQueueItem(int index)
        {
            Player.PlayQueueItem?.Invoke(index);
        }

        public void PlayVideo(MediaItem video)
        {
            Player?.Reset();
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
            yPlayer.VerticalOptions = videoView.VerticalOptions = LayoutOptions.FillAndExpand;
            yPlayer.HorizontalOptions = videoView.HorizontalOptions = LayoutOptions.FillAndExpand;
            this.Children.Insert(0, yPlayer);
            this.Children.Insert(0, videoView);
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
            if (playerType == PlayerType && Player != null)
                return;
            var current = CurrentMedia;
            Player?.Stop();
            Player?.Abort();
            PlayerType = playerType;
            if (playerType == PlayerType.Y)
            {
                Player = yPlayer;
                yPlayer.IsVisible = true;
                videoView.IsVisible = false;
            }
            else
            {
                Player = videoView;
                yPlayer.IsVisible = false;
                videoView.IsVisible = true;
            }

            if (current != null)
                PlayVideo(current);
        }
    }
}

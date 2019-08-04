using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.Content;
using Android.Views;
using Android.Widget;
using MediaManager;
using MediaManager.Media;
using MediaManager.Playback;
using MediaManager.Player;
using MediaManager.Video;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Youtube.Manager.Controls;
using Youtube.Manager.Droid.Models;
using Youtube.Manager.Helper;
using ARelativeLayout = Android.Widget.RelativeLayout;

[assembly: ExportRenderer(typeof(LocalVideoView), typeof(VideoViewRenderer))]


namespace Youtube.Manager.Droid.Models
{
    public class VideoViewRenderer : ViewRenderer<LocalVideoView, ARelativeLayout>, IDisposable
    {
        private ImageView btnPlay;
        private bool fullScrean;
        private ImageView fullscreen_button;
        private SeekBar seeker;
        private TextView video_current_time;
        private TextView video_duration;
        private TextView video_title;
        private ImageView youtube_button_next;
        private ImageView youtube_button_prev;
        private MediaManager.Platforms.Android.Video.VideoView videoView;
        private LocalVideoView element;
        private readonly List<IMediaItem> mediaItems = new List<IMediaItem>();
        private IPlaybackManager PlaybackController => CrossMediaManager.Current;
        private IMediaManager MediaPlayer => CrossMediaManager.Current;


        private bool aborted;
        private long Duration
        {
            get
            {
                try
                {
                    return (int)MediaPlayer.Duration.TotalMilliseconds;
                }
                catch
                {
                    return (int)videoView.Player.Duration;
                }
            }
        }


        public VideoViewRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<LocalVideoView> args)
        {

            if (args.NewElement != null)
                if (Control == null)
                {

                    var vi = LayoutInflater.From(Context);
                    var controller = vi.Inflate(Resource.Layout.LocalVideoView, null);
                    videoView = controller.FindViewById<MediaManager.Platforms.Android.Video.VideoView>(Resource.Id.local_videoview);
                    var relativeLayout = new ARelativeLayout(Context);
                    seeker = controller.FindViewById<SeekBar>(Resource.Id.seek_bar);
                    video_current_time = controller.FindViewById<TextView>(Resource.Id.video_current_time);
                    btnPlay = controller.FindViewById<ImageView>(Resource.Id.y_button);
                    video_duration = controller.FindViewById<TextView>(Resource.Id.video_duration);
                    video_title = controller.FindViewById<TextView>(Resource.Id.video_title);
                    fullscreen_button = controller.FindViewById<ImageView>(Resource.Id.fullscreen_button);
                    youtube_button_prev = controller.FindViewById<ImageView>(Resource.Id.y_button_prev);
                    youtube_button_next = controller.FindViewById<ImageView>(Resource.Id.y_button_next);
                    relativeLayout.AddView(controller);
                    SetNativeControl(relativeLayout);
                    controller.Click += new EventHandler((o, s) => ToggleView());
                    element = args.NewElement;
                    BindEvents();
                    videoView.ShowControls = false;
                    CrossMediaManager.Current.MediaPlayer.VideoView = videoView;
                    videoView.VideoAspect = VideoAspectMode.AspectFill;
                    if (MediaPlayer.State == MediaPlayerState.Playing)
                    {
                        CheckState();
                        PlaybackController_StateChanged(null, new StateChangedEventArgs(MediaPlayerState.Playing));
                    }

                }
        }

        private void ToggleView(bool? value = null)
        {
            var isVisiable = value ?? !(video_current_time.Visibility == ViewStates.Visible);
            if (!isVisiable)
                video_current_time.Visibility =
                    video_title.Visibility =
                        seeker.Visibility =
                            video_duration.Visibility =
                                youtube_button_prev.Visibility =
                                    youtube_button_next.Visibility = fullscreen_button.Visibility = btnPlay.Visibility = ViewStates.Gone;
            else
                video_current_time.Visibility =
                    video_title.Visibility =
                        seeker.Visibility =
                            video_duration.Visibility =
                                youtube_button_prev.Visibility =
                                    youtube_button_next.Visibility = fullscreen_button.Visibility = btnPlay.Visibility = ViewStates.Visible;
        }

        private void Fullscreen_button_Click(object sender, EventArgs e)
        {
            fullScrean = !fullScrean;
            element?.OnFullScrean?.Invoke(fullScrean);
            if (fullScrean)
                MainActivity.Current.Window.AddFlags(WindowManagerFlags.Fullscreen);
            else MainActivity.Current.Window.AddFlags(WindowManagerFlags.TurnScreenOn);

            //ToggleView(!fullScrean);
        }

        protected override void Dispose(bool disposing)
        {
            PlaybackController.MediaItemChanged -= PlaybackController_MediaItemChanged;
            PlaybackController.MediaItemFinished -= PlaybackController_MediaItemFinished;
            PlaybackController.StateChanged -= PlaybackController_StateChanged;
            MediaPlayer.MediaPlayer.BeforePlaying -= MediaPlayer_BeforePlaying;
            videoView.Dispose();
            base.Dispose(disposing);
        }


        public void BindEvents()
        {
            PlaybackController.MediaItemChanged += PlaybackController_MediaItemChanged;
            PlaybackController.MediaItemFinished += PlaybackController_MediaItemFinished;
            PlaybackController.StateChanged += PlaybackController_StateChanged;
            MediaPlayer.MediaPlayer.BeforePlaying += MediaPlayer_BeforePlaying;
            element.Stop = () => PlaybackController.Stop();
            element.Play = () => PlaybackController.Play();
            element.Abort = () => aborted = true;
            element.Reset = () => aborted = false;
            element.PlayQueueItem = PlayQueueItem;
            element.GetCurrentMedia = () => MediaPlayer.MediaQueue.Current != null ? new Youtube.Manager.Models.Container.MediaItem(MediaPlayer.MediaQueue.Current.MediaUri) { Title = MediaPlayer.MediaQueue.Current.Title } : null;
            seeker.ProgressChanged += Seeker_ProgressChanged;
            youtube_button_prev.Click += (sender, e) =>
            {
                PlaybackController.PlayPrevious();
                element?.OnPrev?.Invoke();
            };

            youtube_button_next.Click += (sender, e) =>
            {
                PlaybackController.PlayNext();
                element?.OnNext?.Invoke();
            };

            fullscreen_button.Click += (s, e) => { Fullscreen_button_Click(null, null); };
            btnPlay.Click += TogglePlay;
            element.OnPlayVideo = (items) => PlayVideo(items);
            element.SetFullScrean = (n) =>
             {
                 fullScrean = !n;
                 Fullscreen_button_Click(null, null);
             };
        }

        private async void MediaPlayer_BeforePlaying(object sender, MediaPlayerEventArgs e)
        {
            //await Methods.MediaItemResolver(new Manager.Models.Container.MediaItem(e.MediaItem.MediaUri) { Title = e.MediaItem.Title });

        }

        private void PlaybackController_StateChanged(object sender, StateChangedEventArgs e)
        {
            switch (e.State)
            {
                case MediaPlayerState.Playing:
                    CheckState();
                    btnPlay.SetImageResource(Resource.Drawable.ic_pause_36dp);
                    element.State = Manager.Models.Container.PlayerState.Playing;
                    video_title.SetTextKeepState(element.GetCurrentMedia?.Invoke()?.Title);
                    element.OnVideoStarted?.Invoke(element.GetCurrentMedia());
                    break;

                case MediaPlayerState.Paused:
                    btnPlay.SetImageResource(Resource.Drawable.ic_play_36dp);
                    element.State = Manager.Models.Container.PlayerState.Paused;
                    break;

                case MediaPlayerState.Stopped:
                    element.State = Manager.Models.Container.PlayerState.Stopped;
                    break;
            }
        }

        private void PlaybackController_MediaItemFinished(object sender, MediaItemEventArgs e)
        {
            element.OnVideoEnded?.Invoke();
        }

        private void PlaybackController_MediaItemChanged(object sender, MediaItemEventArgs e)
        {
            CheckState();

        }

        private void Seeker_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            if (e.FromUser)
            {
                long lengthPlayed = Duration * e.Progress / 100;
                videoView.Player.SeekTo((int)lengthPlayed);
            }
        }

        /// <summary>
        ///     Display Time on the player
        /// </summary>
        /// <param name="millis"></param>
        /// <returns></returns>
        private string formatTime(long millis)
        {
            var seconds = millis / 1000;
            var minutes = seconds / 60;
            var hours = minutes / 60;

            return (hours == 0 ? "00:" : hours + ":") + string.Format("{0}:{1}", minutes % 60, seconds % 60);
        }

        private void displayCurrentTime()
        {
            if (null == videoView) return;
            var formattedTime = formatTime((int)(Duration - MediaPlayer.Position.TotalMilliseconds));
            video_current_time.SetTextKeepState(formattedTime);
            var p = MediaPlayer.Position.TotalMilliseconds; //get video position
            var d = Duration; //get video duration
            var c = Convert.ToDouble(p) / Convert.ToDouble(d) * 100; //calculate % complete
            c = Math.Round(Convert.ToDouble(c)); //round to a whole number
            video_duration.SetTextKeepState(formatTime(d));
            seeker.Progress = (int)c;
        }


        public async void PlayQueueItem(int index)
        {
            var item = mediaItems != null && mediaItems.Count > index && index >= 0 ? mediaItems[index] : null;
            if (item != null)
            {
                var v = await MediaPlayer.PlayQueueItem(item);
            }

            await Task.Delay(TimeSpan.FromSeconds(1));
        }

        /// <summary>
        ///  Play Video, todo support for video list
        /// </summary>
        /// <param name="videos"></param>
        public async void PlayVideo(Youtube.Manager.Models.Container.MediaItem[] videos)
        {
            if (CrossMediaManager.Current != null && videos != null)
            {
                mediaItems.Clear();
                mediaItems.AddRange(videos.Select(x => new MediaItem(x.Url)
                {
                    Title = x.Title
                }));
                if (!aborted)
                    await MediaPlayer.Play(mediaItems);
                element.Reset();
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }


        public async void TogglePlay(object sender, EventArgs arg)
        {
            if (MediaPlayer.State == MediaPlayerState.Playing)
                await PlaybackController.Pause();
            else
                await PlaybackController.Play();
        }

        private async void CheckState()
        {
            try
            {
                while (MediaPlayer.State == MediaPlayerState.Playing)
                {
                    displayCurrentTime();
                    if (MediaPlayer.State == MediaPlayerState.Playing)
                        await Task.Delay(TimeSpan.FromSeconds(1));
                }
            }
            catch
            {
            }
        }
    }
}
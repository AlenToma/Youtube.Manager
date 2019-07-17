using System;
using System.Linq;
using System.Threading.Tasks;
using Android.Content;
using Android.Views;
using Android.Widget;
using Google.YouTube.Player;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Youtube.Manager.Controls;
using Youtube.Manager.Droid.Models;
using Youtube.Manager.Helper;
using Youtube.Manager.Models.Container;
using ARelativeLayout = Android.Widget.RelativeLayout;

[assembly: ExportRenderer(typeof(YoutubeVideoView),
    typeof(YoutubeViewRenderer))]

namespace Youtube.Manager.Droid.Models
{
    public class YoutubeViewRenderer :
        ViewRenderer<YoutubeVideoView, ARelativeLayout>,
        IYouTubePlayerOnInitializedListener,
        IYouTubePlayerPlaybackEventListener,
        IYouTubePlayerPlayerStateChangeListener,
        IYouTubePlayerOnFullscreenListener
    {
        private ImageView btnPlay;
        private YoutubeVideoView element;
        private bool fullScrean;
        private ImageView fullscreen_button;
        private SeekBar seeker;
        private TextView video_current_time;
        private TextView video_duration;
        private TextView video_title;
        private ImageView youtube_button_next;
        private ImageView youtube_button_prev;

        private IYouTubePlayer YoutubePlayer;
        private YouTubePlayerSupportFragment youTubePlayerFragment;
        private MediaItem Current;
        private bool aborted;
        public YoutubeViewRenderer(Context context) : base(context)
        {
        }

        // not emplemented
        public void OnFullscreen(bool p0)
        {
            fullScrean = p0;
            if (fullScrean)
                MainActivity.Current.Window.AddFlags(WindowManagerFlags.Fullscreen);
            else MainActivity.Current.Window.AddFlags(WindowManagerFlags.TurnScreenOn);

            YoutubePlayer.SetPlayerStyle(fullScrean
                ? YouTubePlayerPlayerStyle.Minimal
                : YouTubePlayerPlayerStyle.Chromeless);
        }

        public void OnInitializationSuccess(IYouTubePlayerProvider provider, IYouTubePlayer player, bool wasRestored)
        {
            if (!wasRestored && player != null)
            {
                YoutubePlayer = player;
                YoutubePlayer.SetFullscreen(false);
                YoutubePlayer.SetPlayerStyle(YouTubePlayerPlayerStyle.Chromeless); // style
                seeker.ProgressChanged += Seeker_ProgressChanged;
                YoutubePlayer.SetPlaybackEventListener(this);
                YoutubePlayer.SetOnFullscreenListener(this);
                YoutubePlayer.SetPlayerStateChangeListener(this);
                PlayVideo(element.VideoSource);
            }
        }

        public void OnInitializationFailure(IYouTubePlayerProvider p0, YouTubeInitializationResult p1)
        {
            //throw new NotImplementedException();
        }

        public void OnBuffering(bool p0)
        {
            //throw new NotImplementedException();
        }

        public void OnPaused()
        {
            element.State = PlayerState.Paused;
            btnPlay.SetImageResource(Resource.Drawable.ic_play_36dp);
        }

        public void OnPlaying()
        {
            element.State = PlayerState.Playing;
            btnPlay.SetImageResource(Resource.Drawable.ic_pause_36dp);
            CheckState();
        }

        public void OnSeekTo(int p0)
        {
            YoutubePlayer.Play();
            CheckState();
        }

        public void OnStopped()
        {
            btnPlay.SetImageResource(Resource.Drawable.ic_play_36dp);
            element.State = PlayerState.Stopped;
        }

        public void OnAdStarted()
        {
            seeker.Enabled = false;
        }

        public void OnError(YouTubePlayerErrorReason p0)
        {
            if (p0.ToString() != "PLAYER_VIEW_TOO_SMALL") // ignore this shit
                element?.OnError?.Invoke(new Exception(p0.ToString()));
        }

        public void OnLoaded(string p0)
        {
        }

        public void OnLoading()
        {
        }

        public void OnVideoEnded()
        {
            element.State = PlayerState.Stopped;
            element?.OnVideoEnded?.Invoke();
        }

        public void OnVideoStarted()
        {
            element.State = PlayerState.Playing;
            seeker.Enabled = true;
            element?.OnVideoStarted?.Invoke(Current);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<YoutubeVideoView> args)
        {
            base.OnElementChanged(args);
            try
            {
                if (args.OldElement != null) args.OldElement.OnPlayVideo -= PlayVideo;

                if (args.NewElement != null)
                    if (Control == null)
                    {

                        element = args.NewElement;
                        var vi = LayoutInflater.From(Context);
                        var controller = vi.Inflate(Resource.Layout.youtube_manager_controls, null);
                        youTubePlayerFragment = MainActivity.Current.SupportFragmentManager.FindFragmentById(Resource.Id.youtube_fragment) as YouTubePlayerSupportFragment;
                        youTubePlayerFragment.Initialize(Methods.AppSettings.YoutubeDeveloperKey, this);
                        youTubePlayerFragment.RetainInstance = true;
                        var relativeLayout = new ARelativeLayout(Context);
                        seeker = controller.FindViewById<SeekBar>(Resource.Id.seek_bar);
                        video_current_time = controller.FindViewById<TextView>(Resource.Id.video_current_time);
                        btnPlay = controller.FindViewById<ImageView>(Resource.Id.youtube_button);
                        video_duration = controller.FindViewById<TextView>(Resource.Id.video_duration);
                        video_title = controller.FindViewById<TextView>(Resource.Id.video_title);
                        fullscreen_button = controller.FindViewById<ImageView>(Resource.Id.fullscreen_button);
                        youtube_button_prev = controller.FindViewById<ImageView>(Resource.Id.youtube_button_prev);
                        youtube_button_next = controller.FindViewById<ImageView>(Resource.Id.youtube_button_next);
                        btnPlay.Click += TogglePlay;
                        //controller.Click += TogglePlay;
                        youtube_button_prev.Click += (sender, e) => { element?.OnPrev(); };
                        youtube_button_next.Click += (sender, e) => { element?.OnNext(); };
                        element.GetCurrentMedia += () => Current;
                        element.Stop = () => YoutubePlayer?.Pause();
                        element.Play = () => YoutubePlayer?.Play();
                        relativeLayout.AddView(controller);
                        SetNativeControl(relativeLayout);
                        element.OnPlayVideo = PlayVideo;
                        element.Abort = () => aborted = true;
                        element.Reset = () => aborted = false;
                        element.SetFullScrean = n =>
                        {
                            fullScrean = !n;
                            Fullscreen_button_Click(null, null);
                        };
                        fullscreen_button.Click += Fullscreen_button_Click;
                    }
            }
            catch (Exception e)
            {
                Methods.AppSettings.Logger?.Error(e);
            }
        }

        private void Fullscreen_button_Click(object sender, EventArgs e)
        {
            //YoutubePlayer.SetFullscreen(!fullScrean);
            fullScrean = !fullScrean;
            element?.OnFullScrean?.Invoke(fullScrean);
            if (fullScrean)
                MainActivity.Current.Window.AddFlags(WindowManagerFlags.Fullscreen);
            else MainActivity.Current.Window.AddFlags(WindowManagerFlags.TurnScreenOn);

            if (fullScrean)
                video_current_time.Visibility =
                    video_title.Visibility =
                        seeker.Visibility =
                            video_duration.Visibility =
                                youtube_button_prev.Visibility =
                                    youtube_button_next.Visibility = ViewStates.Gone;
            else
                video_current_time.Visibility =
                    video_title.Visibility =
                        seeker.Visibility =
                            video_duration.Visibility =
                                youtube_button_prev.Visibility =
                                    youtube_button_next.Visibility = ViewStates.Visible;
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    var tr = MainActivity.Current.SupportFragmentManager.BeginTransaction();
                    tr?.Remove(youTubePlayerFragment);
                    tr?.Commit();
                    youTubePlayerFragment?.Dispose();
                    YoutubePlayer?.Release();
                    YoutubePlayer?.Dispose();
                }
            }
            catch
            {

            }
            base.Dispose(disposing);
        }


        private void displayCurrentTime()
        {
            if (null == YoutubePlayer) return;
            var formattedTime = formatTime(YoutubePlayer.DurationMillis - YoutubePlayer.CurrentTimeMillis);
            video_current_time.SetTextKeepState(formattedTime);
            var p = YoutubePlayer.CurrentTimeMillis; //get video position
            var d = YoutubePlayer.DurationMillis; //get video duration
            var c = Convert.ToDouble(p) / Convert.ToDouble(d) * 100; //calculate % complete
            c = Math.Round(Convert.ToDouble(c)); //round to a whole number
            seeker.Progress = (int)c;
        }

        /// <summary>
        ///     Display Time on the player
        /// </summary>
        /// <param name="millis"></param>
        /// <returns></returns>
        private string formatTime(int millis)
        {
            var seconds = millis / 1000;
            var minutes = seconds / 60;
            var hours = minutes / 60;

            return (hours == 0 ? "00:" : hours + ":") + string.Format("{0}:{1}", minutes % 60, seconds % 60);
        }

        /// <summary>
        ///     Play Video, todo support for video list
        /// </summary>
        /// <param name="videos"></param>
        public async void PlayVideo(params MediaItem[] videos)
        {

            if (YoutubePlayer != null && videos != null)
            {
                Current = videos.FirstOrDefault();
                video_title.SetTextKeepState(Current.Title);
                //if (!Current.Handled)
                //    await Methods.MediaItemResolver(Current);
                if (!aborted)
                    YoutubePlayer.CueVideo(Current.Url);
                await Task.Delay(TimeSpan.FromSeconds(1));
                if (!aborted)
                    YoutubePlayer.Play();
                video_duration.SetTextKeepState(formatTime(YoutubePlayer.DurationMillis));
                element.Reset();

            }
        }

        private void Seeker_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            if (e.FromUser)
            {
                long lengthPlayed = YoutubePlayer.DurationMillis * e.Progress / 100;
                YoutubePlayer.SeekToMillis((int)lengthPlayed);
            }
        }

        public void TogglePlay(object sender, EventArgs arg)
        {
            if (YoutubePlayer.IsPlaying)
                YoutubePlayer.Pause();
            else YoutubePlayer.Play();
        }

        public void ToggleFullScrean(object sender, EventArgs arg)
        {
            if (YoutubePlayer.IsPlaying)
                YoutubePlayer.SetFullscreen(true);
        }

        private async void CheckState()
        {
            try
            {
                while (YoutubePlayer?.IsPlaying ?? false)
                {
                    displayCurrentTime();


                    if (YoutubePlayer?.IsPlaying ?? false)
                        await Task.Delay(TimeSpan.FromSeconds(1));
                }
            }
            catch
            {
            }
        }
    }
}
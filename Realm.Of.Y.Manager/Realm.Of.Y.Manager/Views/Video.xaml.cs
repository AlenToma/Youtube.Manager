using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaManager;
using Rest.API.Translator;
using Xamarin.Forms;
using Realm.Of.Y.Manager.Controls;
using Realm.Of.Y.Manager.Helper;
using Realm.Of.Y.Manager.Models.Container;
using Realm.Of.Y.Manager.Models.Container.DB_models.Library;
using Realm.Of.Y.Manager.Models.Container.Interface.API;
using Realm.Of.Y.Manager.Views.Template;

namespace Realm.Of.Y.Manager.Views
{
    public partial class Video : PopupBase
    {
        private bool _initialized;
        private VideoWrapper _video;
        private VideoItem lstPlayListVidos;
        private VideoItem lstVidos;
        private string PlayListTitle;
        private double screanHeight;
        private List<VideoWrapper> searchedVideos;

        public Video(VideoWrapper video, List<VideoWrapper> allCollection)
        {
            InitializeComponent();
            _video = video;
            searchedVideos = allCollection;
            screanHeight = Height;

            yVideo.OnFullScrean = b =>
            {
                expandableView.IsVisible = !b && PageO != PageOrientation.Horizontal;
                expandableViewPlayList.IsVisible = _video.HasVideos && !b && PageO != PageOrientation.Horizontal;
            };

            yVideo.OnVideoStarted = v =>
            {
                if (_video.HasVideos && lstPlayListVidos != null && (lstPlayListVidos.SelectedItem != null || lstVidos?.SelectedItem == null))
                    lstPlayListVidos.SelectedItem = _video;
                else if (lstVidos != null)
                    lstVidos.SelectedItem = _video;
            };

            yVideo.OnError = (e) =>
            {
                //Methods.Logger()
            }; // play next and ignore the error 
            yVideo.OnVideoEnded = VideoEnded;
            yVideo.OnNext = VideoEnded;
            yVideo.OnPrev = () => { AutoPlay(false); };
            this.AddTrigger(ControllerRepository.GetInfo<IDbController, Task>(x => x.SaveCategory(null)),
                ControllerRepository.GetInfo<IDbController, Task>(x => x.SaveVideo(null)));
        }


        protected override void OnOrientationChanged(PageOrientation pageOrientation)
        {
            screanHeight = Height;
            if (pageOrientation == PageOrientation.Horizontal)
            {
                yVideo.SetFullScrean?.Invoke(true);
            }
            else
            {
                yVideo.SetFullScrean?.Invoke(false);
                expandableView.IsVisible = true;
                if (expandableViewPlayList.IsExpanded)
                    expandableViewPlayList.HeightRequest = (float)screanHeight / 2;
                else if (expandableView.IsExpanded)
                    expandableView.HeightRequest = (float)screanHeight / 2;
                if (expandableViewPlayList.IsExpanded || expandableView.IsExpanded)
                    yVideo.HeightRequest = (float)screanHeight / 2;
                else yVideo.HeightRequest = -1;
                expandableViewPlayList.IsVisible = _video.HasVideos;
            }
        }


        protected void AutoPlay(bool playNext = true)
        {
            Task.Run(() =>
            {
                var i = playNext ? 1 : -1;
                if (!_video.HasVideos)
                {
                    var index = searchedVideos.FindIndex(x => x.Id == _video.Id);
                    if (index + i < 0 || searchedVideos.Count() <= index + i)
                        index = 0;
                    else index += i;
                    var item = searchedVideos[index];
                    _video = item;
                    if (!_video.IsVideo)
                        LoadData();
                }
                else
                {
                    var index = _video.LoadVideos().Videos.FindIndex(x => x.Id == _video.Id);
                    if (index + i < 0 || _video.Videos.Count() <= index + i)
                        index = 0;
                    else index += i;
                    var item = _video.Videos[index];

                    item.Videos = _video.Videos;
                    _video = item;
                    if (!item.IsVideo)
                        LoadData();
                }

                Play();
            });
        }

        protected void VideoEnded()
        {
            AutoPlay();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            expandableView.StatusChanged += IsExpandChangedHandler;
            expandableViewPlayList.StatusChanged += IsExpandPlayListChangedHandler;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            expandableView.StatusChanged -= IsExpandChangedHandler;
            expandableViewPlayList.StatusChanged -= IsExpandPlayListChangedHandler;
        }


        private void IsExpandPlayListChangedHandler(object sender, StatusChangedEventArgs e)
        {
            if (e.Status == ExpandStatus.Collapsed || e.Status == ExpandStatus.Expanded)
            {
                var rotation = 180;
                if (e.Status == ExpandStatus.Collapsed) rotation = 0;

                if (e.Status == ExpandStatus.Expanded)
                {
                    expandableView.ExpandAnimationLength = 1;
                    expandableView.IsExpanded = false;
                    yVideo.HeightRequest = (float)screanHeight / 2;
                    expandableViewPlayList.HeightRequest = (float)screanHeight / 2 - 50;
                    expandableView.ExpandAnimationLength = 600;
                    LoadlstPlayListVideos();
                }
                else
                {
                    yVideo.HeightRequest = -1;
                    expandableViewPlayList.HeightRequest = -1;
                }
                playListArrow.RotateTo(rotation, 250, Easing.BounceIn);
            }
        }

        private void IsExpandChangedHandler(object sender, StatusChangedEventArgs e)
        {
            if (e.Status == ExpandStatus.Collapsed || e.Status == ExpandStatus.Expanded)
            {
                var rotation = 180;
                if (e.Status == ExpandStatus.Collapsed) rotation = 0;
                if (e.Status == ExpandStatus.Expanded)
                {
                    expandableViewPlayList.ExpandAnimationLength = 1;
                    expandableViewPlayList.IsExpanded = false;
                    yVideo.HeightRequest = (float)screanHeight / 2;
                    expandableView.HeightRequest = (float)screanHeight / 2 - 50;
                    expandableViewPlayList.ExpandAnimationLength = 600;
                    LoadlstVideos();
                }
                else
                {
                    yVideo.HeightRequest = -1;
                    expandableView.HeightRequest = -1;
                }

                arrow.RotateTo(rotation, 250, Easing.BounceIn);
            }
        }

        private Video LoadlstVideos(List<VideoWrapper> lst = null)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var data = lst ?? _video.Videos;
                if (lstVidos == null || lstVidos.SourceItems != lst)
                {
                    if (!expandableView.IsExpanded && lstVidos == null)
                        return;
                    if (lstVidos == null)
                        lstVidos = expandableView.SecondaryView.FindByName<VideoItem>("lstVidos");
                    lstVidos.SourceItems = searchedVideos;
                    lstVidos.HeightRequest = -1;
                    yVideo.OnVideoStarted(null);

                }
            });
            return this;
        }

        private Video LoadlstPlayListVideos(List<VideoWrapper> lst = null)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var data = lst ?? _video.Videos;
                if (lstPlayListVidos == null || lstPlayListVidos.SourceItems != lst)
                {
                    if (!expandableViewPlayList.IsExpanded && lstPlayListVidos == null)
                        return;

                    if (lstPlayListVidos == null)
                        lstPlayListVidos = expandableViewPlayList.SecondaryView.FindByName<VideoItem>("lstVidos");

                    lstPlayListVidos.SourceItems = _video.Videos;
                    lstPlayListVidos.HeightRequest = -1;
                    yVideo.OnVideoStarted(null);


                }
            });
            return this;
        }


        public override async Task DataBinder(MethodInformation method = null)
        {
            lstVidos?.Refresh();
            lstPlayListVidos?.Refresh();
        }

        public Video LoadData()
        {
            if (!_video.IsVideo)
            {
                var title = _video.Title;
                _video.LoadVideos();
                if (!_video.Videos.Any())
                    return this;

                var v = _video.Videos.FirstOrDefault(x => x.IsVideo);

                v.Videos = _video.Videos;
                _video = v;
                PlayListTitle = title;
                LoadlstPlayListVideos();
            }
            else
            {
                var index = searchedVideos.FindIndex(x => x.Id == _video.Id);
                searchedVideos = ControllerRepository.Y(x => x.SearchAsync(UserData.CurrentUser.EntityId.Value, "", 30, 1, _video.Title, VideoSearchType.Mix)).Await().ToItemList(); // get relevant Videos
                searchedVideos.RemoveAll(x => x.Id == _video.Id);
                searchedVideos.Insert(0, _video);
                LoadlstVideos();
            }

            return this;
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            Play();
            return base.OnAppearingAnimationEndAsync();
        }

        public void Play()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                txtPlayTitle.Text = $"{PlayListTitle ?? ""}";
                txtTitle.Text = $"{_video.Title ?? ""}";
                imgPlayList.Source = _video.DefaultThumbnailUrl;
                imgVideo.Source = _video.DefaultThumbnailUrl;
                if (PageO != PageOrientation.Horizontal)
                    expandableViewPlayList.IsVisible = _video.HasVideos;


                if (!_initialized) // first run
                {
                    if (PageO != PageOrientation.Horizontal)
                    {
                        if (_video.HasVideos)
                            expandableViewPlayList.IsExpanded = true;
                        else expandableView.IsExpanded = true;
                    }

                    _initialized = true;
                }

                if (CrossMediaManager.Current?.IsPlaying() ?? false)
                    await CrossMediaManager.Current.Stop();
                yVideo.PlayVideos(new MediaItem(_video.Id) { Title = _video.Title });
            });
        }

        private void LstVidos_OnItemClick(VideoItem obj)
        {
            lstPlayListVidos?.ClearSelection();
            Task.Run(() =>
            {
                var item = obj.SelectedItem;
                if (!item.IsVideo)
                {

                    _video = item;
                    LoadData();
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        lstVidos?.ClearSelection();
                        if (PageO != PageOrientation.Horizontal)
                            expandableViewPlayList.IsExpanded = expandableViewPlayList.IsVisible = true;
                    });
                }
                else
                {
                    _video = item;
                    LoadData();
                }

                Play();
            });
        }

        private void lstPlaylistvideos_OnItemClick(VideoItem obj)
        {
            lstVidos?.ClearSelection();
            Task.Run(() =>
            {
                var item = obj.SelectedItem;
                if (!item.IsVideo)
                {
                    _video = item;
                    LoadData();
                }
                else
                {
                    item.Videos = _video.Videos;
                    _video = item;
                    LoadData();
                }

                Play();
            });
        }
    }
}
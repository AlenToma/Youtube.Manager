using FastDeepCloner;
using Rest.API.Translator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Realm.Of.Y.Manager.Controls;
using Realm.Of.Y.Manager.Helper;
using Realm.Of.Y.Manager.Models.Container;
using Realm.Of.Y.Manager.Models.Container.DB_models;
using Realm.Of.Y.Manager.Models.Container.Interface.API;
using Realm.Of.Y.Manager.Views.Template;

namespace Realm.Of.Y.Manager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocalVideo : PopupBase
    {
        private VideoCategory _category;
        private readonly long _category_Id;
        private readonly string _localPath = UserData.DirectoryManager.DirectoryPath;

        private Dictionary<string, YoutubeFileDownloadItem> _localVideos = new Dictionary<string, YoutubeFileDownloadItem>();

        private LocalVideoItem _videoList;
        private List<VideoData> _videos;

        private VideoData CurrentVideo;
        private double screanHeight;

        public LocalVideo(long categoryId)
        {
            _category_Id = categoryId;
            InitializeComponent();
            this.AddTrigger(
                 ControllerRepository.GetInfo<IDbController, Task>(x => x.SaveCategory(null)),
                 ControllerRepository.GetInfo<IDbController, Task>(x => x.SaveVideo(null)));
            expandableView.IsExpanded = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DataBind();
            lVideo.OnVideoStarted = (item) =>
            {
                if (lVideo.CurrentMedia != null)
                {
                    CurrentVideo = _videos?.FirstOrDefault(x => x.LocalPath == lVideo.CurrentMedia.Url);
                    expandableView.PrimaryView.FindByName<Label>("txtPlayTitle").Text = CurrentVideo?.Title;
                    if (_videoList != null && CurrentVideo != null)
                        _videoList.SelectedItem = CurrentVideo;
                }
            };
            lVideo.OnFullScrean = (n) =>
             {
                 SetFullScrean(n);
             };
        }

        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }


        private void SetFullScrean(bool b)
        {
            expandableView.IsVisible = !b && PageO != PageOrientation.Horizontal;
        }

        protected override void OnOrientationChanged(PageOrientation pageOrientation)
        {

            screanHeight = Height;
            if (pageOrientation == PageOrientation.Horizontal)
            {
                lVideo.SetFullScrean(true);
            }
            else
            {
                lVideo.SetFullScrean(false);
                expandableView.IsVisible = true;
                if (expandableView.IsExpanded)
                    expandableView.HeightRequest = (float)screanHeight / 2;
                if (expandableView.IsExpanded)
                    lVideo.HeightRequest = (float)screanHeight / 2;
                else lVideo.HeightRequest = -1;
            }
        }

        public override async Task DataBinder(MethodInformation method = null)
        {
            _videos = null;
            DataBind();
        }

        public async void DataBind()
        {

            var loader = await this.StartLoading();

            try
            {
                _category = _category ?? UserData.VideoCategoryViews.Find(x => x.EntityId == _category_Id)
;

                _videos = _videos ?? ControllerRepository.Db(x => x.GetVideoData(null, _category_Id, null, 1)).Await();

                var directoryManager = UserData.DirectoryManager.Folder(_category.Name).Create();

                _category.Videos = new List<VideoData>();
                if (!_localVideos.Any())
                {
                    var files = directoryManager.GetFiles();
                    _category.Videos = new List<VideoData>();
                    _localVideos = files.Any()
                        ? files.Select(x => Methods.ParseLocalVideoPath(x.Name)).GroupBy(x => x.VideoId)
                            .Select(x => x.FirstOrDefault()).Where(x => x != null && !string.IsNullOrEmpty(x.VideoId))
                            .ToDictionary(x => x.VideoId, x => x)
                        : new Dictionary<string, YoutubeFileDownloadItem>();

                    foreach (var value in _localVideos.Values)
                    {
                        if (string.IsNullOrEmpty(value.VideoId))
                            continue;
                        var video = _videos.FirstOrDefault(x => x.Video_Id == value.VideoId) ?? ControllerRepository.Db(x => x.GetVideoData(value.VideoId, _category.EntityId, null, 1)).Await().FirstOrDefault();
                        if (video == null)
                        {
                            var videoInfo = ControllerRepository.Y(x => x.GetVideoAsync(value.VideoId, 18)).Await()?.FirstOrDefault();
                            _category.Videos.Add(new VideoData
                            {
                                Title = value.Title.Substring(0, value.Title.IndexOf("[")),
                                Video_Id = value.VideoId,
                                ThumpUrl = $"https://img.youtube.com/vi/{value.VideoId}/0.jpg",
                                Auther = videoInfo?.Auther,
                                Description = videoInfo?.Description,
                                Duration = videoInfo?.Duration,
                                Quality = videoInfo?.Quality,
                                Resolution = videoInfo.Resolution
                            });
                        }
                    }

                    if (_category.Videos.Any())
                    {
                        UserData.SaveCategory(_category);
                        return;
                    }
                }

                foreach (var v in _videos)
                    if (!_localVideos.ContainsKey(v.Video_Id) || string.IsNullOrEmpty(_localVideos[v.Video_Id].Title))
                        v.Playable = false;
                    else
                        v.LocalPath = new Uri(Path.Combine(_localPath, _category.Name, _localVideos[v.Video_Id].Title)).AbsoluteUri;



                if (lVideo.State != PlayerState.Playing)
                    try
                    {
                        Play();
                    }
                    catch
                    {
                        // Ignore
                    }

                SetSource();
            }
            catch (Exception e)
            {
                Methods.AppSettings.Logger?.Error(e);
            }
            finally
            {
                loader.EndLoading();
                //Device.BeginInvokeOnMainThread(() => { Methods.ClearLoader(); });
            }
        }


        private void Play(VideoData video = null)
        {
            try
            {


                if (video == null || !video.Playable)
                    video = _videos.FirstOrDefault(x => x.Playable);


                if (video != null && video.Playable)
                {
                    var index = _videos.FindIndex(x => x == video);
                    var vd = new SafeValueType<string, MediaItem>();

                    foreach (var item in _videos)
                    {
                        if (item.Playable)
                            vd.Add(item.LocalPath, new MediaItem(item.LocalPath)
                            {
                                Title = item.Title,
                            });
                    }

                    CurrentVideo = video;
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (!(lVideo.VideoSource?.Any(x => x.Url == video.LocalPath) ?? false))
                            lVideo.PlayVideos(vd.Values.ToArray());
                        else lVideo.PlayQueueItem(index);
                    });
                }

            }
            catch (Exception e)
            {
                Methods.AppSettings.Logger?.Error(e);
            }
        }

        private async void LstVideos_OnSelection(VideoData arg1, HorizontalList arg2)
        {
            var selectedItem = arg2.SelectedItem as VideoData;
            if (selectedItem.Playable)
            {
                Play(selectedItem);
            }
            else
            {
                arg2.SelectedItem = null;
                lVideo.OnVideoStarted(null);
                await Application.Current.MainPage.DisplayAlert("VDownload".GetString(), "DownloadRequired".GetString(), "Ok".GetString());
            }
        }

        private void SetSource()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (_videoList == null)
                {
                    _videoList = expandableView.SecondaryView.FindByName<LocalVideoItem>("lstVideos");
                    _videoList.HeightRequest = -1;
                }
                if (_videoList != null)
                {
                    _videoList.ItemSource = _videos;
                    lVideo.OnVideoStarted(null);
                }
            });
        }


        private async void ExpandableViewPlayList_IsExpandChanged(object sender, StatusChangedEventArgs e)
        {
            var rotation = 180;
            if (e.Status == ExpandStatus.Collapsed || e.Status == ExpandStatus.Expanded)
            {
                if (e.Status == ExpandStatus.Collapsed) rotation = 0;


                if (e.Status == ExpandStatus.Expanded)
                {
                    lVideo.HeightRequest = (float)screanHeight / 2;
                    expandableView.HeightRequest = (float)screanHeight / 2 - 50;
                    if (_videoList == null)
                        SetSource();
                }
                else
                {
                    lVideo.HeightRequest = -1;
                    expandableView.HeightRequest = -1;
                }

                await expandableView.PrimaryView.FindByName<Image>("playListArrow").RotateTo(rotation, 250, Easing.BounceIn);
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Realm.Of.Y.Manager.Helper;
using Realm.Of.Y.Manager.Models.Container;
using Realm.Of.Y.Manager.Models.Container.DB_models.Library;
using Rest.API.Translator;

namespace Realm.Of.Y.Manager.Views.Template
{
    public partial class VideoItem : ContentView
    {
        public static readonly BindableProperty ItemProperty = BindableProperty.Create(nameof(SourceItems),
            typeof(List<VideoWrapper>), typeof(VideoItem), null, BindingMode.TwoWay,
            propertyChanged: ItemsSourceChanged);

        public Action<VideoWrapper> OnPlayClick;

        public VideoItem()
        {
            this.LoadValueConverters();
            InitializeComponent();
        }

        public List<VideoWrapper> SourceItems
        {
            get => (List<VideoWrapper>)GetValue(ItemProperty);
            set => SetValue(ItemProperty, value);
        }

        public VideoWrapper SelectedItem
        {
            get => hLstVideos.SelectedItem as VideoWrapper;
            set => hLstVideos.SelectedItem = value;
        }


        public bool HeaderIsVisible
        {
            get => stackHeader.IsVisible;
            set => stackHeader.IsVisible = value;
        }

        public string HeaderText
        {
            get => btnHeader.Text;
            set => btnHeader.Text = value;
        }

        public PageType PageType { get; set; }


        public bool PropertyButtonIsVisible { get; set; } = true;

        public ImageSource ImageSource => Category_Id.HasValue && Category_Id > 0
            ? ImageSource.FromFile("download.png")
            : ImageSource.FromFile("showMore.png");

        public long? Category_Id { get; set; }

        public event Action<VideoItem> OnItemClick;

        public event Action<VideoItem> OnLoad;

        public event EventHandler OnHeaderClick;


        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsLayout = (VideoItem)bindable;
            itemsLayout.SetValue();
        }


        private void SetValue()
        {
            OnLoad?.Invoke(this);
            hLstVideos.ItemsSource = SourceItems;
            hLstVideos.HeightRequest = -1;
        }

        public void Refresh()
        {
            hLstVideos.Refresh();
            this.HeightRequest = -1;
        }

        private async void CustomButton_OnPressUp(object sender, EventArgs e)
        {
            if (UserData.CurrentUser == null)
            {
                await Application.Current.MainPage.DisplayAlert("LoginError".GetString(), "NotLoggedIn".GetString(),
                    "Ok".GetString());
                return;
            }

            if (ObjectCacher.DownloadingFiles.Count >= 5)
            {
                await Application.Current.MainPage.DisplayAlert("", "DownloadMax".GetString(), "Ok".GetString());
                return;
            }

            var video = ((Button)sender).BindingContext as VideoWrapper;
            //OnItemClick?.Invoke(this);
            var category = Category_Id.HasValue
                ? UserData.VideoCategoryViews.FirstOrDefault(x => x.EntityId == Category_Id)
                : null;


            var action = "";

            var buttons = new List<string>();
            if (!video.IsVideo)
                buttons.Add("PDownload".GetString());
            else buttons.Add("VDownload".GetString());

            if (OnPlayClick != null)
                buttons.Insert(0, "Play".GetString());

            if (!Category_Id.HasValue)
            {
                action = await Application.Current.MainPage.DisplayActionSheet(" ", "Cancel".GetString(), null, buttons.ToArray());
            }
            else
            {
                if (!video.IsVideo)
                {
                    var vView = await new VideosProperties(video, category.EntityId.Value).DataBind();
                    vView.Open();
                }
                else if (video.IsVideo && UserData.CanDownload(true))
                {
                    Methods.AppSettings.DownloadVideo(video.GetDownloadableItem(category));
                }

                return;
            }

            if (action == "Play".GetString())
                OnPlayClick?.Invoke(video);
            else if (action != "Cancel".GetString()) this.Download(video, PageType);
        }

        public void ClearSelection()
        {
            if (SelectedItem != null)
                SelectedItem = null;
        }

        private void HeaderCustomButton_Clicked(object sender, EventArgs e)
        {
            OnHeaderClick?.Invoke(sender, e);
        }

        private void LstVideos_SelectedItemChanged(object sender, EventArgs e)
        {
            OnItemClick?.Invoke(this);
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            if (this.UserIsLogedIn())
            {
                var loader = await this.StartLoading();
                try
                {
                    var item = (sender as Button).BindingContext as VideoWrapper;
                    if (item == null)
                        return;
                    var action = await Application.Current.MainPage.DisplayAlert("DeleteOperation".GetString(), "VideoDelete".GetString(), "Yes".GetString(), "No".GetString());
                    if (action)
                    {
                        var videos = ControllerRepository.Db(x => x.GetVideoData(item.Id, null, UserData.CurrentUser.EntityId, 1)).Await();
                        videos.ForEach(x => x.State = State.Removed);
                        UserData.SaveVideo(videos.ToArray());
                    }
                }
                catch (Exception ex)
                {
                    Methods.AppSettings.Logger?.Error(ex);
                }
                finally
                {
                    loader.EndLoading();
                }


            }
        }
    }
}
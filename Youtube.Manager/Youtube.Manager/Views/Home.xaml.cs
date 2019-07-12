using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Youtube.Manager.Controls;
using Youtube.Manager.Helper;
using Youtube.Manager.Models.Container;
using Youtube.Manager.Models.Container.DB_models;
using Youtube.Manager.Models.Container.DB_models.Library;
using Youtube.Manager.Models.Container.Interface;
using Youtube.Manager.Models.Container.Interface.API;

namespace Youtube.Manager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage, ModuleTrigger
    {

        public Home()
        {
            this.AddTrigger(ControllerRepository.GetInfo<IDbController, Task>(x => x.SaveVideo(null)))
                .AddTrigger(ControllerRepository.GetInfo<IDbController, Task>(x => x.SaveCategory(null)))
                .AddTrigger(ControllerRepository.GetInfo<IDbController, Models.Container.DB_models.User>(x => x.LogIn(null, null, null)));
            this.LoadValueConverters();
            InitializeComponent();
            DataBinder();

        }

        public async Task DataBinder(MethodInformation method = null)
        {
            if (!this.UserIsLogedIn())
                return;

        
            var l = await this.StartLoading();
            myPlaylist.ItemsSource = UserData.VideoCategoryViews;
            myPlaylist.Header.IsVisible = myPlaylist.HasItems;
            playListSuggesting.ItemsSource = ControllerRepository.Db(x => x.GetUserSuggestion(UserData.CurrentUser.EntityId.Value));
            playListSuggesting.Header.IsVisible = playListSuggesting.HasItems;
            playListUserSeach.ItemsSource = ControllerRepository.Youtube(x => x.SearchAsync(UserData.CurrentUser.EntityId.Value, "", 10, 1, null, VideoSearchType.Recommendation)).Await()?.ToItemList();
            playListUserSeach.Header.IsVisible = playListUserSeach.HasItems;
            adsBanner.IsVisible = UserData.CurrentUser.UserType != UserType.Primary;
            l.EndLoading();

            if (!myPlaylist.HasItems && !UserData.Notified)
            {
                UserData.Notified = true;
                var config = new ToastConfig("BackgroundModeMessage".GetString())
                .SetDuration(TimeSpan.FromSeconds(8))
                .SetMessageTextColor(Color.Red)
                .SetBackgroundColor(Color.WhiteSmoke)
                .SetPosition(ToastPosition.Top);
                UserDialogs.Instance.Toast(config);

            }

            // start the ads
            //await Methods.ReguastNewAdd?.Invoke();
        }

        private async void MyPlaylist_SelectedItemChanged(object sender, EventArgs e)
        {
            if (!await Methods.AppSettings.ValidateStoragePermission())
                return;
            var category = myPlaylist.SelectedItem as VideoCategoryView;
            if (string.IsNullOrEmpty(category.TotalVideosString))
            {
                await Application.Current.MainPage.DisplayAlert("", "The play list is empty", "Ok".GetString());
                return;
            }

            var localVideo = new LocalVideo(category.EntityId.Value);
            myPlaylist.SelectedItem = null;
            localVideo.Open();
        }

        private async void ContextView_Clicked(Controls.MenuItem menu, ContextView sender)
        {
            var id = (long)sender.Identifier;
            VideoCategory category = UserData.VideoCategoryViews.Find(x => x.EntityId == id);
            switch (menu.Identifier)
            {
                case "Remove":
                    var value = await Application.Current.MainPage.DisplayAlert("DeleteOperation".GetString(), "PlayListDelete".GetString(), "Yes".GetString(), "No".GetString());
                    if (value)
                    {
                        category.State = State.Removed;
                        await UserData.SaveCategory(category);
                    }
                    break;
                case "Edit":
                    await UserData.CreateEditPlayList(category);
                    break;
            }
        }

        private void PlayListSuggesting_SelectedItemChanged(object sender, EventArgs e)
        {
            var category = playListSuggesting.SelectedItem as VideoCategoryView;
            var videos = ControllerRepository.Db(x => x.GetVideoData(null, category.EntityId, null, 1)).Await();
            var videoWrappers = videos?.Select(x => new VideoWrapper(x)).ToList();
            var playList = new VideoWrapper() { Title = videoWrappers.First().Title, Videos = videoWrappers, IsVideo = false, IsPlaylist = true };
            new Video(playList, videoWrappers).LoadData().Open();
            playListSuggesting.SelectedItem = null;

        }

        private void _btnLike_Clicked(object sender, EventArgs e)
        {
            var item = (sender as ImageButton).BindingContext as VideoCategoryView;
            ControllerRepository.Db(x => x.Vote(VideoSearchType.PlayList, Ratingtype.Up, item.EntityId.Value, UserData.CurrentUser.EntityId.Value)).Await();
            DataBinder();
        }

        private void _btnDislike_Clicked(object sender, EventArgs e)
        {
            var item = (sender as ImageButton).BindingContext as VideoCategoryView;
            ControllerRepository.Db(x => x.Vote(VideoSearchType.PlayList, Ratingtype.Down, item.EntityId.Value, UserData.CurrentUser.EntityId.Value)).Await();
            DataBinder();

        }

        private void PlayListUserSeach_SelectedItemChanged(object sender, EventArgs e)
        {
            var video = playListUserSeach.SelectedItem as VideoWrapper;
            var videoWrappers = playListUserSeach.ItemsSource as List<VideoWrapper>;
            new Video(video, videoWrappers).LoadData().Open();
            playListUserSeach.SelectedItem = null;
        }
    }
}
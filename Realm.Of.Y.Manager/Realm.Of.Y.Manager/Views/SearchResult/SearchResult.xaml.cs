using Rest.API.Translator;
using Rg.Plugins.Popup.Pages;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Realm.Of.Y.Manager.Helper;
using Realm.Of.Y.Manager.Models.Container;
using Realm.Of.Y.Manager.Models.Container.DB_models.Library;
using Realm.Of.Y.Manager.Models.Container.Interface.API;
using Realm.Of.Y.Manager.Views.Template;

namespace Realm.Of.Y.Manager.Views.SearchResult
{
    public partial class SearchResult : PopupBase
    {
        private readonly string _searchfor;
        private YVideoCollection _videos;
        private readonly VideoSearchType _videoSearchType;

        public SearchResult(VideoSearchType videoSearchType, string searchFor)
        {
            _searchfor = searchFor;
            _videoSearchType = videoSearchType;
            this.AddTrigger(
                ControllerRepository.GetInfo<IDbController, Task>(x => x.SaveCategory(null)),
                ControllerRepository.GetInfo<IDbController, Task>(x => x.SaveVideo(null)));
            InitializeComponent();
        }

        public override async Task DataBinder(MethodInformation method = null)
        {
            var l = await this.StartLoading();
            stVideoContainer.Refresh();
            stVideoContainer.Refresh();
            stVideoContainer.Refresh();
            l.EndLoading();
        }

        public async Task<SearchResult> DataBind()
        {
            var l = await this.StartLoading();
            _videos = ControllerRepository.Y(x => x.SearchAsync(UserData.CurrentUser.EntityId.Value, _searchfor, 20, 1, null, _videoSearchType)).Await();
            if (_videoSearchType == VideoSearchType.Album)
                stVideoContainer.SourceItems = _videos.Albums;
            else if (_videoSearchType == VideoSearchType.PlayList)
                stVideoContainer.SourceItems = _videos.Playlists;
            else stVideoContainer.SourceItems = _videos.Videos;
            l.EndLoading();
            return this;
        }


        private async void StVideoContainer_OnItemClick(VideoItem obj)
        {
            var l = await this.StartLoading();
            var item = obj.SelectedItem;
            Play(item);
            obj.ClearSelection();
            l.EndLoading();
        }

        private async void Play(VideoWrapper video)
        {
            if (video == null)
                return;
            new Video(video, _videos.ToItemList()).LoadData().Open();

        }

        private async void CustomButton_OnPressUp(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var video = _videos.ToItemList().Find(x => x.Id == btn.CommandParameter?.ToString());
            var action = "";
            if (!video.IsVideo)
                action = await DisplayActionSheet(" ", "Cancel".GetString(), null, "Play".GetString(),
                    "PDownload".GetString());
            else
                action = await DisplayActionSheet(" ", "Cancel".GetString(), null, "Play".GetString(),
                    "VDownload".GetString());


            if (action == "Play".GetString())
                Play(video);
            else if (action != "Cancel".GetString()) this.Download(video);
        }
    }
}
using Rest.API.Translator;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Realm.Of.Y.Manager.Helper;
using Realm.Of.Y.Manager.Models.Container;
using Realm.Of.Y.Manager.Models.Container.DB_models.Library;
using Realm.Of.Y.Manager.Models.Container.Interface.API;
using Realm.Of.Y.Manager.Views.Template;

namespace Realm.Of.Y.Manager.Views
{
    public partial class SearchView : PopupBase
    {
        private readonly YVideoCollection _videos;
        private readonly string textSearch;

        public SearchView(YVideoCollection videos, string text)
        {
            InitializeComponent();
            _videos = videos;
            stVideoContainer.HeaderIsVisible = _videos.Playlists.Any();
            lstAlbums.HeaderIsVisible = _videos.Albums.Any();
            stVideoContainer.HeaderIsVisible = _videos.Videos.Any();
            textSearch = text;
            this.AddTrigger(
            ControllerRepository.GetInfo<IDbController, Task>(x => x.SaveCategory(null)),
            ControllerRepository.GetInfo<IDbController, Task>(x => x.SaveVideo(null)));

        }


        protected override void OnAppearing()
        {

            base.OnAppearing();
            DataBinder();
        }

        public override async Task DataBinder(MethodInformation method = null)
        {
            try
            {
                if (method == null)
                {
                    stVideoContainer.SourceItems = _videos.Videos.Take(5).ToList();
                    lstAlbums.SourceItems = _videos.Albums.Take(5).ToList();
                    lstPlayList.SourceItems = _videos.Playlists.Take(5).ToList();
                }
                else
                {
                    stVideoContainer.Refresh();
                    lstAlbums.Refresh();
                    lstPlayList.Refresh();
                }
            }
            catch (Exception e)
            {

            }
        }

        private async void StVideoContainer_OnItemClick(VideoItem obj)
        {
            var l = await this.StartLoading();
            var item = obj.SelectedItem;
            Play(item);
            l.EndLoading();
            obj.SelectedItem = null;
        }

        private void Play(VideoWrapper video)
        {
            if (video == null)
                return;
            new Video(video, _videos.ToItemList()).LoadData().Open();
        }

        private async void VideoTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var l = await this.StartLoading();
            var v = await new SearchResult.SearchResult(VideoSearchType.Videos, textSearch).DataBind();
            v.Open();
            l.EndLoading();
        }

        private async void AlbumsTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var l = await this.StartLoading();
            var v = await new SearchResult.SearchResult(VideoSearchType.Album, textSearch).DataBind();
            v.Open();
            l.EndLoading();
        }

        private async void PlayListTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var l = await this.StartLoading();
            var v = await new SearchResult.SearchResult(VideoSearchType.PlayList, textSearch).DataBind();
            v.Open();
            l.EndLoading();
        }

        private async void CustomButton_Clicked(object sender, EventArgs e)
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
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Realm.Of.Y.Manager.Controls;
using Realm.Of.Y.Manager.Helper;
using Realm.Of.Y.Manager.Models.Container;
using Realm.Of.Y.Manager.Models.Container.DB_models;

namespace Realm.Of.Y.Manager.Views.Template
{
    public partial class LocalVideoItem : ContentView
    {
        public static readonly BindableProperty ItemProperty = BindableProperty.Create(nameof(ItemSource),
            typeof(List<VideoData>), typeof(VideoItem), null, BindingMode.TwoWay, propertyChanged: ItemsSourceChanged);

        public LocalVideoItem()
        {
            this.LoadValueConverters();
            InitializeComponent();
        }

        public List<VideoData> ItemSource
        {
            get => (List<VideoData>)GetValue(ItemProperty);
            set => SetValue(ItemProperty, value);
        }

        public VideoData SelectedItem
        {
            get => lstVideos.SelectedItem as VideoData;
            set => lstVideos.SelectedItem = value;
        }


        public event Action<VideoData, HorizontalList> OnSelection;

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsLayout = (LocalVideoItem)bindable;
            itemsLayout.SetValue();
        }

        private void SetValue()
        {
            lstVideos.ItemsSource = ItemSource;
        }

        private async void CustomButton_Clicked(object sender, EventArgs e)
        {
            var item = (sender as Button).BindingContext as VideoData;
            if (item != null && UserData.CanDownload(true))
                Methods.AppSettings.DownloadVideo(item.GetDownloadableItem());
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            var item = (sender as Button).BindingContext as VideoData;
            if (item == null)
                return;
            var action = await Application.Current.MainPage.DisplayAlert("DeleteOperation".GetString(),
                "VideoDelete".GetString(), "Yes".GetString(), "No".GetString());
            if (action)
            {
                var loader = await this.StartLoading();
                item.State = State.Removed;
                UserData.SaveVideo(item);
                loader.EndLoading();
            }
        }

        private void LstVideos_SelectedItemChanged(object sender, EventArgs e)
        {
            OnSelection?.Invoke(SelectedItem, sender as HorizontalList);
        }
    }
}
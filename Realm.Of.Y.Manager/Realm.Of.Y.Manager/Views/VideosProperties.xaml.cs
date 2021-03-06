﻿using Rest.API.Translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Realm.Of.Y.Manager.Controls;
using Realm.Of.Y.Manager.Helper;
using Realm.Of.Y.Manager.Models.Container;
using Realm.Of.Y.Manager.Models.Container.DB_models;
using Realm.Of.Y.Manager.Models.Container.DB_models.Library;
using Realm.Of.Y.Manager.Models.Container.Interface.API;
using Realm.Of.Y.Manager.Views.Template;

namespace Realm.Of.Y.Manager.Views
{
    public partial class VideosProperties : PopupBase
    {
        public long _categoryId;

        private readonly VideoWrapper _requastedItem;
        private List<VideoWrapper> _videos = new List<VideoWrapper>();

        public VideosProperties(VideoWrapper video, long categoryId)
        {
            InitializeComponent();
            _categoryId = categoryId;
            lstVideosProperties.Category_Id = categoryId;
            _requastedItem = video;
            this.AddTrigger(
            ControllerRepository.GetInfo<IDbController, Task>(x => x.SaveCategory(null)),
            ControllerRepository.GetInfo<IDbController, Task>(x => x.SaveVideo(null)));
        }

        public async Task<VideosProperties> DataBind(bool refresh = false)
        {
            var l = await this.StartLoading();
            if (_requastedItem != null)
            {
                if (!refresh)
                {
                    if (!_requastedItem.IsVideo)
                        _videos = _requastedItem.LoadVideos().Videos;
                    else _videos.Add(_requastedItem);
                }

                lstVideosProperties.SourceItems = _videos;
            }

            l.EndLoading();
            return this;
        }



        public override async Task DataBinder(MethodInformation method = null)
        {
            lstVideosProperties.Refresh();
        }

        private void VideoItem_OnItemClick(VideoItem obj)
        {
            if (_categoryId > 0)
                obj.Category_Id = _categoryId;
        }

        private void VideoItem_OnItemClick_1(VideoItem obj)
        {
            obj.ClearSelection();
        }
    }
}
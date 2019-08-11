using EntityWorker.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Realm.Of.Y.Manager.Models.Container.Interface.API;

namespace Realm.Of.Y.Manager.Models.Container.DB_models.Library
{
    [ExcludeFromAbstract]
    public class VideoCategoryView : VideoCategory
    {
        public long? TotalVideos { get; set; }

        public long? Down_Vote { get; set; }

        public long? Up_Vote { get; set; }

        public string TotalVideosString { get => TotalVideos.HasValue && TotalVideos > 0 ? TotalVideos + " videos" : ""; }

        public string Down_VoteString { get => Down_Vote.RoundAndFormat(); }

        public string Up_VoteString { get => Up_Vote.RoundAndFormat(); }

        public string Image { get => !string.IsNullOrEmpty(Logo) ? ControllerRepository.GetInfo<IYController, object>(a => a.GetImage("")).ToQuary(new FastDeepCloner.SafeValueType<string, object>() { { "imagePath", Logo } }) : ""; }

    }
}

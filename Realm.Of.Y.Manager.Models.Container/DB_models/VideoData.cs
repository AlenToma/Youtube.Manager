using EntityWorker.Core.Attributes;
using System.Collections.Generic;

namespace Realm.Of.Y.Manager.Models.Container.DB_models
{
 #if NETCOREAPP2_2
    [Rule(typeof(Realm.Of.Y.Manager.Models.Container.DB_models.Rules.VideoDataRule))]
#endif
    public class VideoData : Base_Entity
    {
        public string Video_Id { get; set; }

        public string Title { get; set; }

        public List<Rating> Rating { get; set; }

        [ForeignKey(typeof(VideoCategory))]
        public long Category_Id { get; set; }

        public string ThumpUrl { get; set; }

        public string Duration { get; set; }

        public string Description { get; set; }

        public string Auther { get; set; }

        public string Quality { get; set; }

        public string Resolution { get; set; }

        public VideoCategory VideoCategory { get; set; } /// this is so we could inner join VideoCategory

        [ExcludeFromAbstract]
        public bool Playable { get; set; } = true;

        [ExcludeFromAbstract]
        public string LocalPath { get; set; }
    }
}

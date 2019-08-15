using EntityWorker.Core.Attributes;
using System.Collections.Generic;

namespace Realm.Of.Y.Manager.Models.Container.DB_models
{
#if NETCOREAPP2_2
    [Rule(ruleType: typeof(Realm.Of.Y.Manager.Models.Container.DB_models.Rules.CategoryRule))]
#endif
    public class VideoCategory : Base_Entity
    {
        [NotNullable]
        public string Name { get; set; }

        public string Logo { get; set; }

        [ForeignKey(typeof(Realm.Of.Y.Manager.Models.Container.DB_models.User))]
        public long User_Id { get; set; }

        public List<Rating> Rating { get; set; }

        public List<VideoData> Videos { get; set; }
    }
}

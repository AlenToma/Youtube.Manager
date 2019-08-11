﻿using EntityWorker.Core.Attributes;
using System.Collections.Generic;
using Realm.Of.Y.Manager.Models.Container.DB_models.Rules;

namespace Realm.Of.Y.Manager.Models.Container.DB_models
{
    [Rule(ruleType: typeof(CategoryRule))]
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
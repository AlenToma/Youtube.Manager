using EntityWorker.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Realm.Of.Y.Manager.Models.Container.DB_models
{
    public class Rating : Base_Entity
    {
        [Stringify]
        public Ratingtype Ratingtype { get; set; }

        [ForeignKey(typeof(Realm.Of.Y.Manager.Models.Container.DB_models.VideoData))]
        public long? VideoData_Id { get; set; }

        [ForeignKey(typeof(Realm.Of.Y.Manager.Models.Container.DB_models.VideoCategory))]
        public long? Category_Id { get; set; }

        [ForeignKey(typeof(Realm.Of.Y.Manager.Models.Container.DB_models.User))]
        public long User_Id { get; set; }

    }
}

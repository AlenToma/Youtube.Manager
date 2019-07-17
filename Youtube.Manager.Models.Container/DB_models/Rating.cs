using EntityWorker.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Youtube.Manager.Models.Container.DB_models
{
    public class Rating : Base_Entity
    {
        [Stringify]
        public Ratingtype Ratingtype { get; set; }

        [ForeignKey(typeof(VideoData))]
        public long? VideoData_Id { get; set; }

        [ForeignKey(typeof(VideoCategory))]
        public long? Category_Id { get; set; }

        [ForeignKey(typeof(User))]
        public long User_Id { get; set; }

    }
}

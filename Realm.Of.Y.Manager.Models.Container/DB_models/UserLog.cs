using EntityWorker.Core.Attributes;
using System;

namespace Realm.Of.Y.Manager.Models.Container.DB_models
{
    /// <summary>
    /// Uploaded UserLog
    /// </summary>
    public class UserErrorLog : Base_Entity
    {
        [ForeignKey(typeof(DB_models.User))]
        public long User_Id { get; set; }

        public string Text { get; set; }

        public DateTime Added { get; set; }
    }
}

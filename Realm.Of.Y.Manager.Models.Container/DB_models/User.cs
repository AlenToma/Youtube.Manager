using EntityWorker.Core.Attributes;
using System.Collections.Generic;

namespace Realm.Of.Y.Manager.Models.Container.DB_models
{
    public class User : Base_Entity
    {
        // EmailAdress
        [NotNullable]
        public string Email { get; set; }

        public string Password { get; set; }

        public string Picture { get; set; }

        public decimal DownloadCoins { get; set; }

        [Stringify]
        public UserType UserType { get; set; }

        public List<Realm.Of.Y.Manager.Models.Container.DB_models.VideoCategory> VideoCategories { get; set; }
    }
}

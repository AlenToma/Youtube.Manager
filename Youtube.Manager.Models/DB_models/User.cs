using EntityWorker.Core.Attributes;
using System.Collections.Generic;

namespace Youtube.Manager.Models.Container.DB_models
{
    public class User : Base_Entity
    {
        // EmailAdress
        [NotNullable]
        public string Email { get; set; }

        public string Password { get; set; }

        public string Picture { get; set; }

        public long DownloadCoins { get; set; }

        [Stringify]
        public UserType UserType { get; set; }

        public List<VideoCategory> VideoCategories { get; set; }
    }
}

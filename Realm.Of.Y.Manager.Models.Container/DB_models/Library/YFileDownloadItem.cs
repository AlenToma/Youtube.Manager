﻿
namespace Realm.Of.Y.Manager.Models.Container
{
    public class YFileDownloadItem : YVideoInfo
    {
        public string VideoId { get; set; }

        public string Title { get; set; }

        public string Playlist { get; set; }

        public long? category_Id { get; set; }

        public string ThumpUrl { get; set; }
    }
}

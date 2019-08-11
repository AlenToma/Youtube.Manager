using System;
using System.Collections.Generic;
using System.Text;

namespace Realm.Of.Y.Manager.Models.Container
{
    public class MediaItem
    {
        public MediaItem(string url)
        {
            Url = url;
        }

        public bool Handled { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

    }
}

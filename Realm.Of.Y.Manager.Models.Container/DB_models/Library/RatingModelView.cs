using System;
using System.Collections.Generic;
using System.Text;

namespace Realm.Of.Y.Manager.Models.Container
{
    public class RatingModelView
    {
        public string Rating_Up { get; set; }

        public string Rating_Down { get; set; }

        public long Entity_Id { get; set; }

        public VideoSearchType VideoSearchType { get; set; }

    }
}

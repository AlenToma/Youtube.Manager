﻿using EntityWorker.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Realm.Of.Y.Manager.Models.Container.DB_models
{
    public class UserSearch : Base_Entity
    {
        public string Text { get; set; }

        [ForeignKey(typeof(Realm.Of.Y.Manager.Models.Container.DB_models.User))]
        public long User_Id { get; set; }

        // how many times the user search for this
        public long? Counter { get; set; }

    }
}

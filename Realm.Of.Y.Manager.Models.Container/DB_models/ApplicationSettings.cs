using System;
using System.Collections.Generic;
using System.Text;

namespace Realm.Of.Y.Manager.Models.Container.DB_models
{
    public class ApplicationSettings : Base_Entity
    {

        public ApplicationSettings(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }

        public string Value { get; set; }

    }
}

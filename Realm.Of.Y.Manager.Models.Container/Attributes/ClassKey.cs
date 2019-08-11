using System;

namespace Realm.Of.Y.Manager.Models.Container.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ClassKey : Attribute
    {
        public readonly string Name;

        public ClassKey(string name)
        {
            Name = name;

        }
    }
}

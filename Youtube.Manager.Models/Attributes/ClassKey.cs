using System;
using System.Collections.Generic;
using System.Text;

namespace Youtube.Manager.Models.Container.Attributes
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

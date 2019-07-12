using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Youtube.Manager.Models
{
    public class GenericContent<T> : List<T>
    {
        private readonly Action<T> _onAdd;
        private readonly Action _onRefresh;

        public GenericContent(Action<T> onAdd, Action onRefresh)
        {
            _onAdd = onAdd;
            _onRefresh = onRefresh;
        }

        public new void Add(T page)
        {
            base.Add(page);
            _onAdd(page);
        }

        public new void Remove(T page)
        {
            base.Remove(page);
            _onRefresh();
        }

        public new void Clear()
        {
            _onRefresh();
        }

        public new void RemoveAt(int index)
        {
            base.RemoveAt(index);
            _onRefresh();
        }
    }
}
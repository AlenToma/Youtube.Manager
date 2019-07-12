using System;
using System.Collections;
using System.Linq;
using FastDeepCloner;
using Xamarin.Forms;

namespace Youtube.Manager.Controls
{
    public class YListView : ListView
    {
        private bool _invoke = true;

        private int _selectedIndex;
        private IFastDeepClonerProperty _templatedItems;

        public YListView()
        {
            ItemSelected += YListVew_ItemSelected;
        }

        public new IEnumerable ItemsSource
        {
            get => base.ItemsSource;
            set
            {
                BeginRefresh();
                base.ItemsSource = null;
                base.ItemsSource = value;
                EndRefresh();
            }
        }

        public string ItemId { get; set; }

        public int SelectdIndex
        {
            get => _selectedIndex;
            set => Select(value, false);
        }

        public object SelectedObject
        {
            get => SelectedItem;
            set => Select(value);
        }

        private void YListVew_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _selectedIndex = ItemsSource?.Cast<object>().ToList().FindIndex(x => x == e.SelectedItem) ?? -1;
            SelectItem();
            if (_invoke && e.SelectedItem != null)
                OnSelected?.DynamicInvoke(e.SelectedItem, this);

            if (e.SelectedItem != null)
                ScrollTo(e.SelectedItem, ScrollToPosition.MakeVisible, false);
            _invoke = true;
        }

        public event Action<object, YListView> OnSelected;

        public T GetSelected<T>()
        {
            return (T) SelectedItem;
        }

        private void SelectItem()
        {
            _templatedItems = DeepCloner.GetProperty(this?.GetType(), "TemplatedItems");
            //var item = SelectedItem;
            var index = 0;
            if (_templatedItems != null)
            {
                var cells = (ITemplatedItemsList<Cell>) _templatedItems.GetValue(this);

                if (cells == null)
                    return;
                foreach (ViewCell cell in cells)
                {
                    if (cell.BindingContext != null)
                        if (cell.View is StackLayout ||
                            cell.View is Frame || cell.View is ContentView ||
                            cell.View is ContentView || cell.View is Grid ||
                            cell.View is ScrollView)
                        {
                            if (index == SelectdIndex)
                                cell.View.BackgroundColor = Color.FromHex("#6f6f6f");
                            else if (cell.View.BackgroundColor != Color.Default)
                                cell.View.BackgroundColor = Color.Default;
                        }

                    index++;
                }
            }
        }

        public void ClearSelection()
        {
            _invoke = false;
            SelectedItem = null;
        }

        private void Select(int index, bool invoke)
        {
            _invoke = invoke;
            if (ItemsSource == null)
                return;
            _selectedIndex = index;
            if (_selectedIndex < 0)
            {
                _invoke = true;
                SelectItem();
                return;
            }

            if (index >= 0 && index < ItemsSource.Cast<object>().Count())
            {
                SelectedItem = ItemsSource.Cast<object>().ToList()[index];
                SelectItem();
            }

            _invoke = true;
        }

        private void Select(object o)
        {
            if (ItemsSource == null)
                return;

            Select(ItemsSource.Cast<object>().ToList().FindIndex(x => x == o), false);
        }
    }
}
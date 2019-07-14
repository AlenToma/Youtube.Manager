using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Youtube.Manager.Controls
{
    public class HorizontalList : Grid
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(HorizontalList), null);

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource),
            typeof(IEnumerable<object>), typeof(HorizontalList), null, BindingMode.TwoWay,
            propertyChanged: ItemsSourceChanged);

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem),
            typeof(object), typeof(HorizontalList), null, BindingMode.TwoWay, propertyChanged: OnSelectedItemChanged);

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate),
            typeof(DataTemplate), typeof(HorizontalList), default(DataTemplate));

        public static readonly BindableProperty HeaderProperty =
            BindableProperty.Create(nameof(Header), typeof(StackLayout), typeof(HorizontalList), null);

        protected readonly ICommand SelectedCommand;
        protected CScrollView ItemScrollView;
        protected StackLayout ItemsStackLayout;
        protected StackLayout StackContainer;
        protected LoaderIndicator LoaderIndicator;


        public HorizontalList()
        {
            SelectedCommand = new Command<object>(item =>
            {
                if (item == null)
                    return;

                if (item == SelectedItem)
                    return;

                SelectedItem = item;
                SetSelectedItem(item);
            });

            StackContainer = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.Fill
            };

            LoaderIndicator = new LoaderIndicator() { IsVisible = false };
            Children.Add(LoaderIndicator);
            Children.Add(StackContainer);

        }

        public StackOrientation ListOrientation { get; set; }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }


        public Color? SelectedItemColor { get; set; }

        public IEnumerable<object> ItemsSource
        {
            get => (IEnumerable<object>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public StackLayout Header
        {
            get => (StackLayout)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }


        public bool HasItems => ItemsSource != null && ItemsSource.Any();

        public event EventHandler SelectedItemChanged;


        protected virtual void SetItems()
        {
            if (Header != null && Header.IsVisible && ItemScrollView == null) StackContainer.Children.Add(Header);


            if (ItemScrollView == null)
            {
                ItemScrollView = new CScrollView
                {
                    Orientation = ListOrientation == StackOrientation.Horizontal
                        ? ScrollOrientation.Horizontal
                        : ScrollOrientation.Vertical,
                    VerticalScrollBarVisibility = ScrollBarVisibility.Never,
                    HorizontalScrollBarVisibility = ScrollBarVisibility.Never
                };

                if (ListOrientation == StackOrientation.Horizontal)
                    ItemScrollView.HorizontalOptions = LayoutOptions.FillAndExpand;
                else ItemScrollView.VerticalOptions = LayoutOptions.FillAndExpand;

                ItemsStackLayout = new StackLayout
                {
                    Orientation = ListOrientation,
                    Padding = Padding,
                    Spacing = 2,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                ItemScrollView.Content = ItemsStackLayout;
                StackContainer.Children.Add(ItemScrollView);
            }
            SaveScroll();
            ItemsStackLayout.Children.Clear();

            if (!HasItems)
                return;
            LoaderIndicator.IsVisible = true;
            Task.Run(() =>
            {
                var lstViews = new List<View>();
                foreach (var item in ItemsSource)
                    lstViews.Add(GetItemView(item));

                Device.BeginInvokeOnMainThread(() =>
                {
                    lstViews.ForEach(x => ItemsStackLayout.Children.Add(x));
                    if (SelectedItem != null)
                        SetUiSelection(SelectedItem, true);
                    else SetScroll();

                    LoaderIndicator.IsVisible = false;
                });
            });
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsLayout = (HorizontalList)bindable;
            itemsLayout.SetItems();
        }


        protected virtual View GetItemView(object item)
        {
            var content = ItemTemplate.CreateContent();
            View view = null;
            if (content is View)
                view = content as View;
            else if (content is ContentView)
                view = content as ContentView;
            else return null;
            var gesture = new TapGestureRecognizer
            {
                Command = SelectedCommand,
                CommandParameter = item
            };
            AddGesture(view, gesture, 8);
            view.BindingContext = item;
            return view;
        }

        public void Refresh()
        {
            if (HasItems)
            {
                var selectedItem = SelectedItem;
                var items = ItemsSource;
                ItemsSource = null;
                ItemsSource = items;
                SelectedItem = selectedItem;
            }
        }


        protected int AddGesture(View view, TapGestureRecognizer gesture, int counter = 3)
        {
            if (counter <= 0)
                return counter;
            view.GestureRecognizers.Add(gesture);
            var layout = view as Layout<View>;

            counter--;
            if (layout == null)
            {
                if (view is ScrollView && ((ScrollView)view).Content != null)
                {
                    AddGesture(((ScrollView)view).Content, gesture, counter);
                }
                return counter;
            }

            foreach (var child in layout.Children)
            {
                if (counter <= 0)
                    break;
                counter = AddGesture(child, gesture, counter);
            }
            return counter;
        }

        protected virtual void SetSelectedItem(object selectedItem, bool triggerOnChange = true)
        {
            SetUiSelection(selectedItem);
            var handler = SelectedItemChanged;
            if (handler != null && triggerOnChange)
                handler(this, EventArgs.Empty);
        }

        private double ScrollPosition;
        private void SaveScroll()
        {
            ScrollPosition = ListOrientation == StackOrientation.Vertical ? ItemScrollView.ScrollY : ItemScrollView.ScrollX;
        }

        private void SetScroll()
        {
            if (ListOrientation == StackOrientation.Vertical)
                ItemScrollView.ScrollToAsync(0, ScrollPosition, false);
            else ItemScrollView.ScrollToAsync(ScrollPosition, 0, false);
        }

        public async void SetUiSelection(object selectedItem, bool scrollToSelectedItem = false)
        {
            if (!HasItems)
                return;
            var index = selectedItem == null ? -1 : ItemsSource.ToList().FindIndex(x => x == selectedItem);
            var i = 0;
            foreach (object view in ItemsStackLayout.Children)
            {
                var v = view as View;
                if (v == null)
                    v = view as ContentView;


                if (v == null)
                    continue;

                if (!SelectedItemColor.HasValue)
                    SelectedItemColor = v.BackgroundColor;

                if (i == index)
                {
                    v.BackgroundColor = (Color)Application.Current.Resources["SelectedItem"];
                    if (scrollToSelectedItem)
                    {
                        if (ListOrientation == StackOrientation.Vertical)
                            await ItemScrollView.ScrollToAsync(0, v.Height * index, false);
                        else await ItemScrollView.ScrollToAsync(v.Width * index, 0, false);
                    }
                }
                else if (v.BackgroundColor != SelectedItemColor.Value)
                {
                    v.BackgroundColor = SelectedItemColor.Value;
                }

                i++;
            }
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsView = (HorizontalList)bindable;
            if (newValue == oldValue)
                return;

            itemsView.SetSelectedItem(newValue, false);
        }
    }
}
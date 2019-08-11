using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Realm.Of.Y.Manager.Helper;
using Realm.Of.Y.Manager.Models;

namespace Realm.Of.Y.Manager.Controls
{
    public class TabContent
    {
        public bool ButtonIsVisiable { get; set; } = true;

        public CustomButton Button { get; set; }

        public View View
        {
            get
            {
                if (PageContent != null)
                    return PageContent.Content;
                else return ContentView;
            }
            private set { ContentView = value; }
        }


        private string _title;
        public string Title
        {
            get
            {
                if (PageContent != null && _title == null)
                    return PageContent.Title;
                else
                {
                    return _title;
                }
            }
            set
            {
                _title = value;
            }
        }


        public bool IsVisible
        {
            get
            {
                return ((View) View.Parent).IsVisible;
            }
            set
            {
                ((View)View.Parent).IsVisible = value;
            }

        }

        private ImageSource _icon;

        public ImageSource Icon
        {
            get
            {
                if (PageContent != null && _icon == null)
                    return PageContent.IconImageSource;
                else
                {
                    return _icon;
                }
            }
            set
            {
                _icon = value;
            }
        }

        public ContentPage PageContent { get; set; }

        public View ContentView { get; set; }
    }

    public class TappedView : RelativeLayout
    {
        public int _selectedIndex;

        private readonly StackLayout buttons;

        private Thickness? topParentPadding;

        public TappedView()
        {
            TabContents = new GenericContent<TabContent>(p => DataBind(p), Refresh);
            var height = 25;
            Style = (Style)Application.Current.Resources["LayoutFloatLeft"];
            VerticalOptions = LayoutOptions.FillAndExpand;
            buttons = new StackLayout
            {
                Style = (Style)Application.Current.Resources["FormFloatLeft"],
                BackgroundColor = (Color)Application.Current.Resources["barBackgroundColor"],
                VerticalOptions = LayoutOptions.End,
                HeightRequest = height,
                Padding = new Thickness(0, 0, 0, 0)
            };

            Children.Add(buttons, null, Constraint.RelativeToParent(parent =>
            {
                return GetHeight(parent);
                //return Application.Current.MainPage.Height - (buttons.HeightRequest + this.Y);
            }), Constraint.RelativeToParent(parent => { return parent.Width; }), null);
        }

        public GenericContent<TabContent> TabContents { get; set; }

        // minimumSize in Procent to the mainPage
        public double MinSize { get; set; } = 0.8;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                Select();
            }
        }

        public int ItemCount { get; private set; }
        public event Action<TappedView> OnTapChanged;


        public double GetHeight(View parent)
        {
            var padding = 1; /*+ topParentPadding?.Bottom ?? 0 + topParentPadding?.VerticalThickness ?? 0*/
            ;
            double position;
            if (parent.Parent as View != null)
                position = (parent.Parent as View).Height - (buttons.HeightRequest + Y) - padding;
            else position = parent.Height - (buttons.Height + Y) - padding;
            if (position > Application.Current.MainPage.Height - (buttons.HeightRequest + Y) - padding || position < 0)
                position = Application.Current.MainPage.Height - (buttons.HeightRequest + Y) - padding;

            if (MinSize * position <
                MinSize * (Application.Current.MainPage.Height - (buttons.HeightRequest + Y) - padding))
                position = Application.Current.MainPage.Height - (buttons.HeightRequest + Y) - padding;
            return position;
        }

        public void DisableView(int selectedIndex)
        {
            TabContents[selectedIndex].IsVisible = false;
            Refresh();

        }

        public void EnableView(int selectedIndex)
        {
            TabContents[selectedIndex].IsVisible = true;
            Refresh();

        }

        private void Refresh()
        {
            while (Children.Count > 1)
                Children.RemoveAt(1);
            buttons.Children.Clear();
            TabContents.ForEach(x => DataBind(x));

            if (!TabContents[SelectedIndex].IsVisible)
            {
                if (TabContents.Any(x => x.IsVisible))
                {
                    SelectedIndex = TabContents.FindIndex(x => x.IsVisible);
                }
            }
            if (buttons.Children.Count <= 0 || buttons.Children.Where(x => x.IsVisible).Count() <= 1)
                buttons.IsVisible = false;
            else buttons.IsVisible = true;
        }

        private void DataBind(TabContent page)
        {
            var stk = new ScrollView
            {
                Style = (Style)Application.Current.Resources["LayoutFloatLeft"],
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = ScrollOrientation.Vertical
            };
            stk.Content = page.View;


            Children.Add(stk, null, Constraint.RelativeToParent(parent => { return 0; }),
                Constraint.RelativeToParent(parent => { return parent.Width; }), Constraint.RelativeToParent(parent =>
                {
                    if (topParentPadding == null)
                    {
                        var view = parent.GetAbsoluteParent<StackLayout>();
                        if (view != null)
                            topParentPadding = view.Padding;
                        else topParentPadding = new Thickness();
                    }

                    var padding = topParentPadding.Value.Bottom + buttons.Height + Y;
                    double position;
                    if (parent.Parent as View != null)
                        position = (parent.Parent as View).Height - padding;
                    else position = parent.Height - padding;
                    if (position > Application.Current.MainPage.Height - padding || position < 0)
                        position = Application.Current.MainPage.Height - padding;


                    if (MinSize * position < MinSize * (Application.Current.MainPage.Height - padding))
                        position = Application.Current.MainPage.Height - padding;
                    return position;
                }));
            var btn = new CustomButton
            {
                Padding = new Thickness(0, 0, 0, 0),
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            page.Button = btn;
            if (page.Icon != null)
                btn.ImageSource = page.Icon;


            if (!string.IsNullOrEmpty(page.Title))
            {
                btn.Text = page.Title;
                btn.Padding = new Thickness(5, 0, 5, 0);
                btn.FontSize = 12;
            }
            else
            {
                btn.FontSize = 0;
            }

            btn.Style = (Style)Application.Current.Resources["ButtonContainer"];
            btn.TextColor = (Color)Application.Current.Resources["text"];
            btn.Clicked += (o, arg) => { SelectedIndex = buttons.Children.IndexOf(btn); };
            btn.IsVisible = page.ButtonIsVisiable;
            buttons.Children.Add(btn);
            Select();
            ItemCount = buttons.Children.Count;
        }

        public void Select()
        {
            var i = 0;
            foreach (var content in TabContents)
            {
                if (SelectedIndex == i)
                {
                    content.IsVisible = true;
                    if (content.ButtonIsVisiable)
                        content.Button.IsSelected = true;
                }
                else
                {
                    if (content.ButtonIsVisiable)
                        content.Button.IsSelected = false;
                    content.IsVisible = false;
                }
                i++;
            }
            OnTapChanged?.Invoke(this);
        }
    }
}
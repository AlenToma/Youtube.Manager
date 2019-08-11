using System;
using System.Linq;
using Xamarin.Forms;
using Realm.Of.Y.Manager.Helper;

namespace Realm.Of.Y.Manager.Controls
{
    public class ButtonContextMenu : RelativeLayout
    {
        public static readonly BindableProperty ItemViewProperty = BindableProperty.Create(nameof(ItemView),
            typeof(DataTemplate), typeof(ButtonContextMenu), default(DataTemplate), BindingMode.TwoWay, null,
            ItemViewchanged);

        public static readonly BindableProperty AbsoluteParentProperty = BindableProperty.Create(nameof(AbsoluteParent),
            typeof(View), typeof(ButtonContextMenu), null, BindingMode.TwoWay, null);

        public static readonly BindableProperty ItemContextProperty = BindableProperty.Create(nameof(ItemContext),
            typeof(DataTemplate), typeof(ButtonContextMenu), default(DataTemplate), BindingMode.TwoWay, null,
            ItemContextchanged);

        protected Constraint constraint;

        protected AbsoluteLayout Container;

        protected Swaper LeftContent;

        protected RelativeLayout RightContent;


        private bool swaptLeft;

        public ButtonContextMenu()
        {
            Container = new AbsoluteLayout();
            //Orientation = ScrollOrientation.Horizontal;
            VerticalOptions = LayoutOptions.Fill;
            HorizontalOptions = LayoutOptions.Fill;
            //VerticalScrollBarVisibility = ScrollBarVisibility.Never;
            //HorizontalScrollBarVisibility = ScrollBarVisibility.Never;
            Container.Children.Add(ViewStack = new StackLayout
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Spacing = 0,
                Orientation = StackOrientation.Horizontal
            });
            Children.Insert(0, Container);
        }


        /// <summary>
        ///     This is soth the text context know the width of the parent
        ///     This is still optional
        /// </summary>
        public View AbsoluteParent
        {
            get => (View) GetValue(AbsoluteParentProperty);
            set => SetValue(AbsoluteParentProperty, value);
        }

        public DataTemplate ItemView
        {
            get => (DataTemplate) GetValue(ItemViewProperty);
            set => SetValue(ItemViewProperty, value);
        }

        public DataTemplate ItemContext
        {
            get => (DataTemplate) GetValue(ItemContextProperty);
            set => SetValue(ItemContextProperty, value);
        }

        /// <summary>
        ///     Default is 200
        /// </summary>
        public uint EasingLength { get; set; } = 100;

        protected StackLayout ViewStack { get; }

        private static void ItemContextchanged(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsLayout = (ButtonContextMenu) bindable;
            itemsLayout.SetItemContextView();
        }

        private static void ItemViewchanged(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsLayout = (ButtonContextMenu) bindable;
            itemsLayout.SetItemView();
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            var size = base.OnMeasure(widthConstraint, heightConstraint);
            if (swaptLeft)
            {
                swaptLeft = false;
                var leftBound = new Rectangle(0, LeftContent.Bounds.Y, LeftContent.Bounds.Width,
                    LeftContent.Bounds.Height);
                LeftContent.LayoutTo(leftBound, 1, null);

                var rightBounds =
                    new Rectangle(size.Request.Width > widthConstraint ? widthConstraint : size.Request.Width,
                        RightContent.Bounds.Y, RightContent.Bounds.Width, RightContent.Bounds.Height);
                RightContent.LayoutTo(rightBounds, 1, null);
                //Swap(new Swaper().Swap(SwipeDirection.Right, 0, 0));
            }


            return size;
        }

        public async void Swap(Swaper swaper)
        {
            var contextWidth = RightContent.Width + 13;

            if (RightContent.Width <= 15)
                return;


            if (swaper.Disable().SwipeDirection == SwipeDirection.Left && !swaptLeft)
            {
                var leftBound = new Rectangle(LeftContent.Bounds.X - contextWidth, LeftContent.Bounds.Y,
                    LeftContent.Bounds.Width, LeftContent.Bounds.Height);
                await LeftContent.LayoutTo(leftBound, EasingLength, Easing.CubicInOut);

                var rightBounds = new Rectangle(RightContent.Bounds.X - contextWidth, RightContent.Bounds.Y,
                    RightContent.Bounds.Width, RightContent.Bounds.Height);
                await RightContent.LayoutTo(rightBounds, EasingLength, Easing.CubicInOut);
                swaptLeft = true;
            }
            else if (swaper.SwipeDirection == SwipeDirection.Right && swaptLeft)
            {
                var leftBound = new Rectangle(LeftContent.Bounds.X + contextWidth, LeftContent.Bounds.Y,
                    LeftContent.Bounds.Width, LeftContent.Bounds.Height);
                await LeftContent.LayoutTo(leftBound, EasingLength, Easing.CubicInOut);


                var rightBounds = new Rectangle(RightContent.Bounds.X + contextWidth, RightContent.Bounds.Y,
                    RightContent.Bounds.Width, RightContent.Bounds.Height);
                await RightContent.LayoutTo(rightBounds, EasingLength, Easing.CubicInOut);
                swaptLeft = false;
            }

            swaper.Enable();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            SetBinding();
        }


        private void SetBinding()
        {
            if (AbsoluteParent == null)
                AbsoluteParent = this.GetAbsoluteParent(e => e is HorizontalList || e is ListView);
            if (AbsoluteParent != null)
            {
                if (!LeftContent.Children.Any())
                    LeftContent.SetBinding(WidthRequestProperty, new Binding("Width", source: AbsoluteParent));
                else
                    LeftContent.Children[0].SetBinding(WidthRequestProperty, new Binding("Width", source: AbsoluteParent));
            }
        }


        public void SetItemView()
        {
            var leftContent = ItemView.CreateContent();
            var view = leftContent as View;
            if (view == null)
                view = leftContent as ContentView;
            if (view == null)
                throw new Exception("Only a view or Content View is supported");


            LeftContent = new Swaper
            {
                Children = {view},
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill
            };
            LeftContent.OnSwap += Swap;
            ViewStack.Children.Add(LeftContent);
            if (AbsoluteParent != null)
                SetBinding();
        }


        protected void SetItemContextView()
        {
            var rightContent = ItemContext.CreateContent();
            var rView = rightContent as View;
            if (rView == null)
                rView = rightContent as ContentView;
            if (rView == null)
                throw new Exception("Only a view or Content View is supported");


            RightContent = new RelativeLayout
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill
            };

            RightContent.Children.Insert(0, rView);
            ViewStack.Children.Add(RightContent);
        }
    }
}
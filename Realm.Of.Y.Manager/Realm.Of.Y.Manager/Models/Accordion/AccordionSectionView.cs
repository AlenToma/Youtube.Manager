using System.Collections;
using Xamarin.Forms;

namespace Realm.Of.Y.Manager.Models.Accordion
{
    public class AccordionSectionView : StackLayout
    {
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(
                "ItemsSource",
                typeof(IList),
                typeof(AccordionSectionView),
                default(IList),
                propertyChanged: PopulateList);

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(
                "Title",
                typeof(string),
                typeof(AccordionSectionView),
                propertyChanged: ChangeTitle);

        private readonly ImageSource _arrowDown = ImageSource.FromFile("ic_keyboard_arrow_down_white_24dp.png");
        private readonly ImageSource _arrowRight = ImageSource.FromFile("ic_keyboard_arrow_right_white_24dp.png");
        private readonly StackLayout _content = new StackLayout {HeightRequest = 0};
        private readonly AbsoluteLayout _header = new AbsoluteLayout();
        private readonly Color _headerColor = Color.FromHex("0067B7");
        private readonly Image _headerIcon = new Image {VerticalOptions = LayoutOptions.Center};

        private readonly Label _headerTitle = new Label
            {TextColor = Color.White, VerticalTextAlignment = TextAlignment.Center, HeightRequest = 50};

        private bool _isExpanded;
        private readonly DataTemplate _template;

        public AccordionSectionView(DataTemplate itemTemplate, ScrollView parent)
        {
            _template = itemTemplate;
            _headerTitle.BackgroundColor = _headerColor;
            _headerIcon.Source = _arrowRight;
            _header.BackgroundColor = _headerColor;

            _header.Children.Add(_headerIcon, new Rectangle(0, 1, .1, 1), AbsoluteLayoutFlags.All);
            _header.Children.Add(_headerTitle, new Rectangle(1, 1, .9, 1), AbsoluteLayoutFlags.All);

            Spacing = 0;
            Children.Add(_header);
            Children.Add(_content);

            _header.GestureRecognizers.Add(
                new TapGestureRecognizer
                {
                    Command = new Command(async () =>
                    {
                        if (_isExpanded)
                        {
                            _headerIcon.Source = _arrowRight;
                            _content.HeightRequest = 0;
                            _content.IsVisible = false;
                            _isExpanded = false;
                        }
                        else
                        {
                            _headerIcon.Source = _arrowDown;
                            _content.HeightRequest = _content.Children.Count * 50;
                            _content.IsVisible = true;
                            _isExpanded = true;

                            // Scroll top by the current Y position of the section
                            if (parent.Parent is VisualElement) await parent.ScrollToAsync(0, Y, true);
                        }
                    })
                }
            );
        }

        public IList ItemsSource
        {
            get => (IList) GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public string Title
        {
            get => (string) GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        private void ChangeTitle()
        {
            _headerTitle.Text = Title;
        }

        private void PopulateList()
        {
            _content.Children.Clear();

            foreach (var item in ItemsSource)
            {
                var template = (View) _template.CreateContent();
                template.BindingContext = item;
                _content.Children.Add(template);
            }
        }

        private static void ChangeTitle(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue == newValue) return;
            ((AccordionSectionView) bindable).ChangeTitle();
        }

        private static void PopulateList(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue == newValue) return;
            ((AccordionSectionView) bindable).PopulateList();
        }
    }
}
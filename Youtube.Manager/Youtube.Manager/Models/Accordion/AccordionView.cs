using System.Collections;
using Xamarin.Forms;

namespace Youtube.Manager.Models.Accordion
{
    public class AccordionView : ScrollView
    {
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(
                "ItemsSource",
                typeof(IList),
                typeof(AccordionSectionView),
                default(IList),
                propertyChanged: PopulateList);

        private readonly StackLayout _layout = new StackLayout {Spacing = 1};

        public AccordionView(DataTemplate itemTemplate)
        {
            SubTemplate = itemTemplate;
            Template = new DataTemplate(() => (object) new AccordionSectionView(itemTemplate, this));
            Content = _layout;
        }

        public DataTemplate Template { get; set; }
        public DataTemplate SubTemplate { get; set; }

        public IList ItemsSource
        {
            get => (IList) GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        private void PopulateList()
        {
            _layout.Children.Clear();

            foreach (var item in ItemsSource)
            {
                var template = (View) Template.CreateContent();
                template.BindingContext = item;
                _layout.Children.Add(template);
            }
        }

        private static void PopulateList(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue == newValue) return;
            ((AccordionView) bindable).PopulateList();
        }
    }
}
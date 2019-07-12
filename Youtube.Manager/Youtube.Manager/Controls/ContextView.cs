using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Youtube.Manager.Controls
{

    public class MenuItem
    {
        public string Text { get; set; }

        public string Identifier { get; set; }

        public ImageSource Image { get; set; }
    }

    public class ContextView : View
    {
        public static readonly BindableProperty IdentifierProperty = BindableProperty.Create(nameof(Identifier), typeof(object), typeof(ContextView), null);
        public object Identifier { get => GetValue(IdentifierProperty); set => SetValue(IdentifierProperty, value); }

        public Action<MenuItem> Click;

        public event Action<MenuItem, ContextView> Clicked;

        public string Title { get; set; }

        public List<MenuItem> Menus { get; set; }


        public ContextView()
        {
            Menus = new List<MenuItem>();
            Click = (e) =>
             {
                 Clicked?.Invoke(e, this);
             };
        }

    }
}

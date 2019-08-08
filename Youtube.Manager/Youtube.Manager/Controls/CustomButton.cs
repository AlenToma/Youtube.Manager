using System;
using Xamarin.Forms;

namespace Youtube.Manager.Controls
{
    public class CustomButton : Button
    {
        public bool _isSelected;
        private Color ColorBeforeSelection;
        public new Action<double, double> SizeAllocated;

        public CustomButton()
        {
            ColorBeforeSelection = Color.Transparent;
        }

        public Color OnPressColor { get; set; } = (Color) Application.Current.Resources["pressed"];
        public TextAlignment? TextAlignment { get; set; }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;

                if (_isSelected)
                {
                    if (BackgroundColor != OnPressColor)
                        ColorBeforeSelection = BackgroundColor;
                    BackgroundColor = OnPressColor;
                }
                else
                {
                    BackgroundColor = ColorBeforeSelection;
                }
            }
        }


        public event EventHandler OnPressDown;

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            SizeAllocated?.Invoke(width, height);
        }

        public void OnPressed()
        {
            OnPressDown?.Invoke(this, null);
        }
    }
}
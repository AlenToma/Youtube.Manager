using System;
using Xamarin.Forms;

namespace Youtube.Manager.Controls
{
    public class Swaper : StackLayout
    {
        public Action<bool> OnStateChanged;

        private bool swaping;

        public new float X { get; private set; }

        public new float Y { get; private set; }

        public SwipeDirection SwipeDirection { get; private set; }

        public new bool IsEnabled { get; private set; } = true;

        public event Action<Swaper> OnSwap;


        public Swaper Enable()
        {
            IsEnabled = true;
            OnStateChanged?.Invoke(true);
            return this;
        }

        public Swaper Disable()
        {
            IsEnabled = false;
            OnStateChanged?.Invoke(false);
            return this;
        }

        public Swaper Swap(SwipeDirection direction, float x, float y)
        {
            if (!swaping && IsEnabled)
            {
                X = x;
                Y = y;
                SwipeDirection = direction;
                swaping = true;
                OnSwap?.Invoke(this);
                swaping = false;
            }

            return this;
        }
    }
}
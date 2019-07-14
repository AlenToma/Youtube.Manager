using System;
using System.Timers;
using Android.Content;
using Android.OS;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Youtube.Manager.Controls;
using Youtube.Manager.Droid.Models;

[assembly: ExportRenderer(typeof(Swaper), typeof(SwaperRenderer))]

namespace Youtube.Manager.Droid.Models
{
    public class SwaperRenderer : VisualElementRenderer<StackLayout>
    {
        private Context context;
        private Handler h2 = new Handler();
        private Swaper horizontalSwaper;
        private Timer timer;
        private float x1, y1;


        public SwaperRenderer(Context context) : base(context)
        {
            this.context = context;
        }


        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);

            horizontalSwaper = e.NewElement as Swaper;

            Touch += ButtonContextMenuRenderer_Touch;
            timer = new Timer {Interval = 500, AutoReset = false};
            timer.Elapsed += (o, a) => { CScrollViewRenderer.IsDisabled = false; };
        }

        private void ButtonContextMenuRenderer_Touch(object sender, TouchEventArgs e)
        {
         
            if (horizontalSwaper.IsEnabled)
                switch (e.Event.Action)
                {
                    case MotionEventActions.Up:
                    case MotionEventActions.Cancel:
                    case MotionEventActions.ButtonRelease:
                        horizontalSwaper.Opacity = 1;
                        CScrollViewRenderer.IsDisabled = false;
                        //timer.Start();
                        break;

                    default:
                        var test = "";
                        break;
                    case MotionEventActions.Down:
                    case MotionEventActions.ButtonPress:
                        x1 = e.Event.GetX();
                        y1 = e.Event.GetY();
                        horizontalSwaper.Opacity = 0.6f;
                        break;

                    case MotionEventActions.Move:
                        var movement = e.Event.GetX() - x1;
                        var offset = 50;
                        var yMovement = e.Event.GetY() - y1;

                        if (yMovement <= 15 && yMovement >= 9 || Math.Abs(movement) > offset)
                        {
                            timer.Stop();
                            CScrollViewRenderer.IsDisabled = true;
                        }

                        if (Math.Abs(movement) > offset)
                        {
                            if (movement < 0)
                            {
                                horizontalSwaper.Swap(SwipeDirection.Left, e.Event.GetX(), e.Event.GetY());
                                Console.WriteLine("Left swipe");
                            }
                            else
                            {
                                horizontalSwaper.Swap(SwipeDirection.Right, e.Event.GetX(), e.Event.GetY());
                                Console.WriteLine("Right swipe");
                            }

                            horizontalSwaper.Opacity = 1;
                        }
                        else
                        {
                            if (Math.Abs(yMovement) > offset)
                            {
                                if (movement < 0)
                                {
                                    horizontalSwaper.Swap(SwipeDirection.Down, e.Event.GetX(), e.Event.GetY());
                                    Console.WriteLine("Left swipe");
                                    //webView.GoBack();
                                }
                                else
                                {
                                    horizontalSwaper.Swap(SwipeDirection.Up, e.Event.GetX(), e.Event.GetY());
                                    Console.WriteLine("Right swipe");
                                    //webView.GoForward();
                                }

                                horizontalSwaper.Opacity = 1;
                            }
                        }

                        break;
                }
            if (CScrollViewRenderer.IsDisabled)
                timer.Start();

            e.Handled = false;
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            return true;
        }
    }
}
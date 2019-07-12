namespace Youtube.Manager.Controls
{
    public abstract class PlatformHelper
    {
        public static PlatformHelper Instance { get; private set; }

        public static void InitializeSingleton(PlatformHelper instance)
        {
            Instance = instance;
        }

        public abstract int DpToPixels(int dp);

        public abstract int DpToPixels(double dp);

        public abstract int PixelsToDp(int pixels);
    }
}
namespace Realm.Of.Y.Manager.Models.Container
{
    public enum VideoSearchType { All, Videos, PlayList, Album, Mix, Channel, Recommendation, Rating }

    public enum VideoType { Video, Playlist, Channel }

    public enum State { Added, Removed }

    public enum LogLevel { Release, Debug }

    public enum PageType { Popup, DisplayActionSheet }

    public enum PlayerState
    {
        Stopped,
        Playing,
        Paused,
    }

    public enum PlayerType
    {
        Youtube,
        VideoView
    }

    public enum PageOrientation
    {
        Horizontal = 0,
        Vertical = 1,
    }

    public enum Float { Right, Left }

    public enum Ratingtype { Down, Up }

    /// <summary>
    /// Primary= User who paid for the app
    /// User = normal user eg Download coins and ads applied
    /// </summary>
    public enum UserType { User, Premium, System }
}

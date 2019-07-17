using System;

namespace Youtube.Manager.Models.Container.Interface
{
    public interface IPlayer
    {
        PlayerState State { get; }

        Action<Exception> OnError { get; set; }

        Action<bool> OnFullScrean { get; set; }

        Action OnNext { get; set; }

        Action<MediaItem[]> OnPlayVideo { get; set; }

        Action OnPrev { get; set; }

        Action OnVideoEnded { get; set; }

        Action<MediaItem> OnVideoStarted { get; set; }

        Action<bool> SetFullScrean { get; set; }

        Func<MediaItem> GetCurrentMedia { get; }

        Action Stop { get; }

        Action Play { get; }

        Action Abort { get; }

        Action Reset { get; set; }

        Action<int> PlayQueueItem { get; set; }
    }
}

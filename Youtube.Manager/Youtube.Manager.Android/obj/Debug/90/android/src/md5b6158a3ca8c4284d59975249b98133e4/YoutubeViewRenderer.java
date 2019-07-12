package md5b6158a3ca8c4284d59975249b98133e4;


public class YoutubeViewRenderer
	extends md51558244f76c53b6aeda52c8a337f2c37.ViewRenderer_2
	implements
		mono.android.IGCUserPeer,
		com.google.android.youtube.player.YouTubePlayer.OnInitializedListener,
		com.google.android.youtube.player.YouTubePlayer.PlaybackEventListener,
		com.google.android.youtube.player.YouTubePlayer.PlayerStateChangeListener,
		com.google.android.youtube.player.YouTubePlayer.OnFullscreenListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onInitializationFailure:(Lcom/google/android/youtube/player/YouTubePlayer$Provider;Lcom/google/android/youtube/player/YouTubeInitializationResult;)V:GetOnInitializationFailure_Lcom_google_android_youtube_player_YouTubePlayer_Provider_Lcom_google_android_youtube_player_YouTubeInitializationResult_Handler:Google.YouTube.Player.IYouTubePlayerOnInitializedListenerInvoker, YouTubeApi\n" +
			"n_onInitializationSuccess:(Lcom/google/android/youtube/player/YouTubePlayer$Provider;Lcom/google/android/youtube/player/YouTubePlayer;Z)V:GetOnInitializationSuccess_Lcom_google_android_youtube_player_YouTubePlayer_Provider_Lcom_google_android_youtube_player_YouTubePlayer_ZHandler:Google.YouTube.Player.IYouTubePlayerOnInitializedListenerInvoker, YouTubeApi\n" +
			"n_onBuffering:(Z)V:GetOnBuffering_ZHandler:Google.YouTube.Player.IYouTubePlayerPlaybackEventListenerInvoker, YouTubeApi\n" +
			"n_onPaused:()V:GetOnPausedHandler:Google.YouTube.Player.IYouTubePlayerPlaybackEventListenerInvoker, YouTubeApi\n" +
			"n_onPlaying:()V:GetOnPlayingHandler:Google.YouTube.Player.IYouTubePlayerPlaybackEventListenerInvoker, YouTubeApi\n" +
			"n_onSeekTo:(I)V:GetOnSeekTo_IHandler:Google.YouTube.Player.IYouTubePlayerPlaybackEventListenerInvoker, YouTubeApi\n" +
			"n_onStopped:()V:GetOnStoppedHandler:Google.YouTube.Player.IYouTubePlayerPlaybackEventListenerInvoker, YouTubeApi\n" +
			"n_onAdStarted:()V:GetOnAdStartedHandler:Google.YouTube.Player.IYouTubePlayerPlayerStateChangeListenerInvoker, YouTubeApi\n" +
			"n_onError:(Lcom/google/android/youtube/player/YouTubePlayer$ErrorReason;)V:GetOnError_Lcom_google_android_youtube_player_YouTubePlayer_ErrorReason_Handler:Google.YouTube.Player.IYouTubePlayerPlayerStateChangeListenerInvoker, YouTubeApi\n" +
			"n_onLoaded:(Ljava/lang/String;)V:GetOnLoaded_Ljava_lang_String_Handler:Google.YouTube.Player.IYouTubePlayerPlayerStateChangeListenerInvoker, YouTubeApi\n" +
			"n_onLoading:()V:GetOnLoadingHandler:Google.YouTube.Player.IYouTubePlayerPlayerStateChangeListenerInvoker, YouTubeApi\n" +
			"n_onVideoEnded:()V:GetOnVideoEndedHandler:Google.YouTube.Player.IYouTubePlayerPlayerStateChangeListenerInvoker, YouTubeApi\n" +
			"n_onVideoStarted:()V:GetOnVideoStartedHandler:Google.YouTube.Player.IYouTubePlayerPlayerStateChangeListenerInvoker, YouTubeApi\n" +
			"n_onFullscreen:(Z)V:GetOnFullscreen_ZHandler:Google.YouTube.Player.IYouTubePlayerOnFullscreenListenerInvoker, YouTubeApi\n" +
			"";
		mono.android.Runtime.register ("Youtube.Manager.Droid.Models.YoutubeViewRenderer, Youtube.Manager.Android", YoutubeViewRenderer.class, __md_methods);
	}


	public YoutubeViewRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == YoutubeViewRenderer.class)
			mono.android.TypeManager.Activate ("Youtube.Manager.Droid.Models.YoutubeViewRenderer, Youtube.Manager.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public YoutubeViewRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == YoutubeViewRenderer.class)
			mono.android.TypeManager.Activate ("Youtube.Manager.Droid.Models.YoutubeViewRenderer, Youtube.Manager.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public YoutubeViewRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == YoutubeViewRenderer.class)
			mono.android.TypeManager.Activate ("Youtube.Manager.Droid.Models.YoutubeViewRenderer, Youtube.Manager.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public void onInitializationFailure (com.google.android.youtube.player.YouTubePlayer.Provider p0, com.google.android.youtube.player.YouTubeInitializationResult p1)
	{
		n_onInitializationFailure (p0, p1);
	}

	private native void n_onInitializationFailure (com.google.android.youtube.player.YouTubePlayer.Provider p0, com.google.android.youtube.player.YouTubeInitializationResult p1);


	public void onInitializationSuccess (com.google.android.youtube.player.YouTubePlayer.Provider p0, com.google.android.youtube.player.YouTubePlayer p1, boolean p2)
	{
		n_onInitializationSuccess (p0, p1, p2);
	}

	private native void n_onInitializationSuccess (com.google.android.youtube.player.YouTubePlayer.Provider p0, com.google.android.youtube.player.YouTubePlayer p1, boolean p2);


	public void onBuffering (boolean p0)
	{
		n_onBuffering (p0);
	}

	private native void n_onBuffering (boolean p0);


	public void onPaused ()
	{
		n_onPaused ();
	}

	private native void n_onPaused ();


	public void onPlaying ()
	{
		n_onPlaying ();
	}

	private native void n_onPlaying ();


	public void onSeekTo (int p0)
	{
		n_onSeekTo (p0);
	}

	private native void n_onSeekTo (int p0);


	public void onStopped ()
	{
		n_onStopped ();
	}

	private native void n_onStopped ();


	public void onAdStarted ()
	{
		n_onAdStarted ();
	}

	private native void n_onAdStarted ();


	public void onError (com.google.android.youtube.player.YouTubePlayer.ErrorReason p0)
	{
		n_onError (p0);
	}

	private native void n_onError (com.google.android.youtube.player.YouTubePlayer.ErrorReason p0);


	public void onLoaded (java.lang.String p0)
	{
		n_onLoaded (p0);
	}

	private native void n_onLoaded (java.lang.String p0);


	public void onLoading ()
	{
		n_onLoading ();
	}

	private native void n_onLoading ();


	public void onVideoEnded ()
	{
		n_onVideoEnded ();
	}

	private native void n_onVideoEnded ();


	public void onVideoStarted ()
	{
		n_onVideoStarted ();
	}

	private native void n_onVideoStarted ();


	public void onFullscreen (boolean p0)
	{
		n_onFullscreen (p0);
	}

	private native void n_onFullscreen (boolean p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}

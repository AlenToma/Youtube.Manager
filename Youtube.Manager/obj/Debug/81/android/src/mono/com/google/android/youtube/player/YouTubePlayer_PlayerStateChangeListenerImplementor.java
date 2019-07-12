package mono.com.google.android.youtube.player;


public class YouTubePlayer_PlayerStateChangeListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.youtube.player.YouTubePlayer.PlayerStateChangeListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAdStarted:()V:GetOnAdStartedHandler:YouTube.Player.IYouTubePlayerPlayerStateChangeListenerInvoker, YouTube.Player.Android\n" +
			"n_onError:(Lcom/google/android/youtube/player/YouTubePlayer$ErrorReason;)V:GetOnError_Lcom_google_android_youtube_player_YouTubePlayer_ErrorReason_Handler:YouTube.Player.IYouTubePlayerPlayerStateChangeListenerInvoker, YouTube.Player.Android\n" +
			"n_onLoaded:(Ljava/lang/String;)V:GetOnLoaded_Ljava_lang_String_Handler:YouTube.Player.IYouTubePlayerPlayerStateChangeListenerInvoker, YouTube.Player.Android\n" +
			"n_onLoading:()V:GetOnLoadingHandler:YouTube.Player.IYouTubePlayerPlayerStateChangeListenerInvoker, YouTube.Player.Android\n" +
			"n_onVideoEnded:()V:GetOnVideoEndedHandler:YouTube.Player.IYouTubePlayerPlayerStateChangeListenerInvoker, YouTube.Player.Android\n" +
			"n_onVideoStarted:()V:GetOnVideoStartedHandler:YouTube.Player.IYouTubePlayerPlayerStateChangeListenerInvoker, YouTube.Player.Android\n" +
			"";
		mono.android.Runtime.register ("YouTube.Player.IYouTubePlayerPlayerStateChangeListenerImplementor, YouTube.Player.Android", YouTubePlayer_PlayerStateChangeListenerImplementor.class, __md_methods);
	}


	public YouTubePlayer_PlayerStateChangeListenerImplementor ()
	{
		super ();
		if (getClass () == YouTubePlayer_PlayerStateChangeListenerImplementor.class)
			mono.android.TypeManager.Activate ("YouTube.Player.IYouTubePlayerPlayerStateChangeListenerImplementor, YouTube.Player.Android", "", this, new java.lang.Object[] {  });
	}


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

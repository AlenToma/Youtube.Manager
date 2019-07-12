package mono.com.google.android.youtube.player;


public class YouTubePlayer_OnFullscreenListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.youtube.player.YouTubePlayer.OnFullscreenListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onFullscreen:(Z)V:GetOnFullscreen_ZHandler:YouTube.Player.IYouTubePlayerOnFullscreenListenerInvoker, YouTube.Player.Android\n" +
			"";
		mono.android.Runtime.register ("YouTube.Player.IYouTubePlayerOnFullscreenListenerImplementor, YouTube.Player.Android", YouTubePlayer_OnFullscreenListenerImplementor.class, __md_methods);
	}


	public YouTubePlayer_OnFullscreenListenerImplementor ()
	{
		super ();
		if (getClass () == YouTubePlayer_OnFullscreenListenerImplementor.class)
			mono.android.TypeManager.Activate ("YouTube.Player.IYouTubePlayerOnFullscreenListenerImplementor, YouTube.Player.Android", "", this, new java.lang.Object[] {  });
	}


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

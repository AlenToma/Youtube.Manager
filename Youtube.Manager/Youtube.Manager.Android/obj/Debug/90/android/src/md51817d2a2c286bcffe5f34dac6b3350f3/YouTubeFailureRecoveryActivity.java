package md51817d2a2c286bcffe5f34dac6b3350f3;


public abstract class YouTubeFailureRecoveryActivity
	extends com.google.android.youtube.player.YouTubeBaseActivity
	implements
		mono.android.IGCUserPeer,
		com.google.android.youtube.player.YouTubePlayer.OnInitializedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onActivityResult:(IILandroid/content/Intent;)V:GetOnActivityResult_IILandroid_content_Intent_Handler\n" +
			"n_onInitializationFailure:(Lcom/google/android/youtube/player/YouTubePlayer$Provider;Lcom/google/android/youtube/player/YouTubeInitializationResult;)V:GetOnInitializationFailure_Lcom_google_android_youtube_player_YouTubePlayer_Provider_Lcom_google_android_youtube_player_YouTubeInitializationResult_Handler:Google.YouTube.Player.IYouTubePlayerOnInitializedListenerInvoker, YouTubeApi\n" +
			"n_onInitializationSuccess:(Lcom/google/android/youtube/player/YouTubePlayer$Provider;Lcom/google/android/youtube/player/YouTubePlayer;Z)V:GetOnInitializationSuccess_Lcom_google_android_youtube_player_YouTubePlayer_Provider_Lcom_google_android_youtube_player_YouTubePlayer_ZHandler:Google.YouTube.Player.IYouTubePlayerOnInitializedListenerInvoker, YouTubeApi\n" +
			"";
		mono.android.Runtime.register ("Youtube.Manager.Droid.YouTubeFailureRecoveryActivity, Youtube.Manager.Android", YouTubeFailureRecoveryActivity.class, __md_methods);
	}


	public YouTubeFailureRecoveryActivity ()
	{
		super ();
		if (getClass () == YouTubeFailureRecoveryActivity.class)
			mono.android.TypeManager.Activate ("Youtube.Manager.Droid.YouTubeFailureRecoveryActivity, Youtube.Manager.Android", "", this, new java.lang.Object[] {  });
	}


	public void onActivityResult (int p0, int p1, android.content.Intent p2)
	{
		n_onActivityResult (p0, p1, p2);
	}

	private native void n_onActivityResult (int p0, int p1, android.content.Intent p2);


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

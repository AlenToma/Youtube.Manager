package md5801c40edb0276ef08d62b58baf13c6be;


public class AndroidMediaPlayer
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("MediaManager.Platforms.Android.Media.AndroidMediaPlayer, MediaManager", AndroidMediaPlayer.class, __md_methods);
	}


	public AndroidMediaPlayer ()
	{
		super ();
		if (getClass () == AndroidMediaPlayer.class)
			mono.android.TypeManager.Activate ("MediaManager.Platforms.Android.Media.AndroidMediaPlayer, MediaManager", "", this, new java.lang.Object[] {  });
	}

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

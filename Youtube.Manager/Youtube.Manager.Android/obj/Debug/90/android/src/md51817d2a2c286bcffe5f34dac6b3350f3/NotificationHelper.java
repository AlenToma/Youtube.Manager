package md51817d2a2c286bcffe5f34dac6b3350f3;


public class NotificationHelper
	extends android.content.ContextWrapper
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Youtube.Manager.Droid.NotificationHelper, Youtube.Manager.Android", NotificationHelper.class, __md_methods);
	}


	public NotificationHelper (android.content.Context p0)
	{
		super (p0);
		if (getClass () == NotificationHelper.class)
			mono.android.TypeManager.Activate ("Youtube.Manager.Droid.NotificationHelper, Youtube.Manager.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
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

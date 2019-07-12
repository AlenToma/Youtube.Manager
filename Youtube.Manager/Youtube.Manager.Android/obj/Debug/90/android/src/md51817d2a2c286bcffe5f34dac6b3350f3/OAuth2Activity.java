package md51817d2a2c286bcffe5f34dac6b3350f3;


public class OAuth2Activity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Youtube.Manager.Droid.OAuth2Activity, Youtube.Manager.Android", OAuth2Activity.class, __md_methods);
	}


	public OAuth2Activity ()
	{
		super ();
		if (getClass () == OAuth2Activity.class)
			mono.android.TypeManager.Activate ("Youtube.Manager.Droid.OAuth2Activity, Youtube.Manager.Android", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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

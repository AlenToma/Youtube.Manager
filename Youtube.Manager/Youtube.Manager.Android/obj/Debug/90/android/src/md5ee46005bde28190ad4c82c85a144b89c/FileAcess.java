package md5ee46005bde28190ad4c82c85a144b89c;


public class FileAcess
	extends android.os.Binder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Youtube.Manager.Droid.Services.FileAcess, Youtube.Manager.Android", FileAcess.class, __md_methods);
	}


	public FileAcess ()
	{
		super ();
		if (getClass () == FileAcess.class)
			mono.android.TypeManager.Activate ("Youtube.Manager.Droid.Services.FileAcess, Youtube.Manager.Android", "", this, new java.lang.Object[] {  });
	}

	public FileAcess (md5ee46005bde28190ad4c82c85a144b89c.FileService p0)
	{
		super ();
		if (getClass () == FileAcess.class)
			mono.android.TypeManager.Activate ("Youtube.Manager.Droid.Services.FileAcess, Youtube.Manager.Android", "Youtube.Manager.Droid.Services.FileService, Youtube.Manager.Android", this, new java.lang.Object[] { p0 });
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

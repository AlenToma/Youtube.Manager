package com.google.android.exoplayer2.source;


public class DynamicConcatenatingMediaSource_MediaSourceHolder
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		java.lang.Comparable
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_compareTo:(Ljava/lang/Object;)I:GetCompareTo_Ljava_lang_Object_Handler:Java.Lang.IComparableInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Com.Google.Android.Exoplayer2.Source.DynamicConcatenatingMediaSource+MediaSourceHolder, ExoPlayer.Core", DynamicConcatenatingMediaSource_MediaSourceHolder.class, __md_methods);
	}


	public DynamicConcatenatingMediaSource_MediaSourceHolder ()
	{
		super ();
		if (getClass () == DynamicConcatenatingMediaSource_MediaSourceHolder.class)
			mono.android.TypeManager.Activate ("Com.Google.Android.Exoplayer2.Source.DynamicConcatenatingMediaSource+MediaSourceHolder, ExoPlayer.Core", "", this, new java.lang.Object[] {  });
	}


	public int compareTo (java.lang.Object p0)
	{
		return n_compareTo (p0);
	}

	private native int n_compareTo (java.lang.Object p0);

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

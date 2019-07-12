package mono.com.google.android.exoplayer2.upstream;


public class TransferListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.exoplayer2.upstream.TransferListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onBytesTransferred:(Ljava/lang/Object;I)V:GetOnBytesTransferred_Ljava_lang_Object_IHandler:Com.Google.Android.Exoplayer2.Upstream.ITransferListenerInvoker, ExoPlayer.Core\n" +
			"n_onTransferEnd:(Ljava/lang/Object;)V:GetOnTransferEnd_Ljava_lang_Object_Handler:Com.Google.Android.Exoplayer2.Upstream.ITransferListenerInvoker, ExoPlayer.Core\n" +
			"n_onTransferStart:(Ljava/lang/Object;Lcom/google/android/exoplayer2/upstream/DataSpec;)V:GetOnTransferStart_Ljava_lang_Object_Lcom_google_android_exoplayer2_upstream_DataSpec_Handler:Com.Google.Android.Exoplayer2.Upstream.ITransferListenerInvoker, ExoPlayer.Core\n" +
			"";
		mono.android.Runtime.register ("Com.Google.Android.Exoplayer2.Upstream.ITransferListenerImplementor, ExoPlayer.Core", TransferListenerImplementor.class, __md_methods);
	}


	public TransferListenerImplementor ()
	{
		super ();
		if (getClass () == TransferListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Google.Android.Exoplayer2.Upstream.ITransferListenerImplementor, ExoPlayer.Core", "", this, new java.lang.Object[] {  });
	}


	public void onBytesTransferred (java.lang.Object p0, int p1)
	{
		n_onBytesTransferred (p0, p1);
	}

	private native void n_onBytesTransferred (java.lang.Object p0, int p1);


	public void onTransferEnd (java.lang.Object p0)
	{
		n_onTransferEnd (p0);
	}

	private native void n_onTransferEnd (java.lang.Object p0);


	public void onTransferStart (java.lang.Object p0, com.google.android.exoplayer2.upstream.DataSpec p1)
	{
		n_onTransferStart (p0, p1);
	}

	private native void n_onTransferStart (java.lang.Object p0, com.google.android.exoplayer2.upstream.DataSpec p1);

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

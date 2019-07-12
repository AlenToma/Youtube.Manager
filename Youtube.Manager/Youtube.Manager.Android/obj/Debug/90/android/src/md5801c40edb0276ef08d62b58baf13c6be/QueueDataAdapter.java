package md5801c40edb0276ef08d62b58baf13c6be;


public class QueueDataAdapter
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.exoplayer2.ext.mediasession.TimelineQueueEditor.QueueDataAdapter
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_add:(ILandroid/support/v4/media/MediaDescriptionCompat;)V:GetAdd_ILandroid_support_v4_media_MediaDescriptionCompat_Handler:Com.Google.Android.Exoplayer2.Ext.Mediasession.TimelineQueueEditor/IQueueDataAdapterInvoker, ExoPlayer.Ext.MediaSession\n" +
			"n_getMediaDescription:(I)Landroid/support/v4/media/MediaDescriptionCompat;:GetGetMediaDescription_IHandler:Com.Google.Android.Exoplayer2.Ext.Mediasession.TimelineQueueEditor/IQueueDataAdapterInvoker, ExoPlayer.Ext.MediaSession\n" +
			"n_move:(II)V:GetMove_IIHandler:Com.Google.Android.Exoplayer2.Ext.Mediasession.TimelineQueueEditor/IQueueDataAdapterInvoker, ExoPlayer.Ext.MediaSession\n" +
			"n_remove:(I)V:GetRemove_IHandler:Com.Google.Android.Exoplayer2.Ext.Mediasession.TimelineQueueEditor/IQueueDataAdapterInvoker, ExoPlayer.Ext.MediaSession\n" +
			"";
		mono.android.Runtime.register ("MediaManager.Platforms.Android.Media.QueueDataAdapter, MediaManager", QueueDataAdapter.class, __md_methods);
	}


	public QueueDataAdapter ()
	{
		super ();
		if (getClass () == QueueDataAdapter.class)
			mono.android.TypeManager.Activate ("MediaManager.Platforms.Android.Media.QueueDataAdapter, MediaManager", "", this, new java.lang.Object[] {  });
	}

	public QueueDataAdapter (com.google.android.exoplayer2.source.ConcatenatingMediaSource p0)
	{
		super ();
		if (getClass () == QueueDataAdapter.class)
			mono.android.TypeManager.Activate ("MediaManager.Platforms.Android.Media.QueueDataAdapter, MediaManager", "Com.Google.Android.Exoplayer2.Source.ConcatenatingMediaSource, ExoPlayer.Core", this, new java.lang.Object[] { p0 });
	}


	public void add (int p0, android.support.v4.media.MediaDescriptionCompat p1)
	{
		n_add (p0, p1);
	}

	private native void n_add (int p0, android.support.v4.media.MediaDescriptionCompat p1);


	public android.support.v4.media.MediaDescriptionCompat getMediaDescription (int p0)
	{
		return n_getMediaDescription (p0);
	}

	private native android.support.v4.media.MediaDescriptionCompat n_getMediaDescription (int p0);


	public void move (int p0, int p1)
	{
		n_move (p0, p1);
	}

	private native void n_move (int p0, int p1);


	public void remove (int p0)
	{
		n_remove (p0);
	}

	private native void n_remove (int p0);

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

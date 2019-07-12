package md53a9783d4b6b7797aec892ced2ce87c62;


public class RewardedVideoAdListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.ads.reward.RewardedVideoAdListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onRewarded:(Lcom/google/android/gms/ads/reward/RewardItem;)V:GetOnRewarded_Lcom_google_android_gms_ads_reward_RewardItem_Handler:Android.Gms.Ads.Reward.IRewardedVideoAdListenerInvoker, Xamarin.GooglePlayServices.Ads.Lite\n" +
			"n_onRewardedVideoAdClosed:()V:GetOnRewardedVideoAdClosedHandler:Android.Gms.Ads.Reward.IRewardedVideoAdListenerInvoker, Xamarin.GooglePlayServices.Ads.Lite\n" +
			"n_onRewardedVideoAdFailedToLoad:(I)V:GetOnRewardedVideoAdFailedToLoad_IHandler:Android.Gms.Ads.Reward.IRewardedVideoAdListenerInvoker, Xamarin.GooglePlayServices.Ads.Lite\n" +
			"n_onRewardedVideoAdLeftApplication:()V:GetOnRewardedVideoAdLeftApplicationHandler:Android.Gms.Ads.Reward.IRewardedVideoAdListenerInvoker, Xamarin.GooglePlayServices.Ads.Lite\n" +
			"n_onRewardedVideoAdLoaded:()V:GetOnRewardedVideoAdLoadedHandler:Android.Gms.Ads.Reward.IRewardedVideoAdListenerInvoker, Xamarin.GooglePlayServices.Ads.Lite\n" +
			"n_onRewardedVideoAdOpened:()V:GetOnRewardedVideoAdOpenedHandler:Android.Gms.Ads.Reward.IRewardedVideoAdListenerInvoker, Xamarin.GooglePlayServices.Ads.Lite\n" +
			"n_onRewardedVideoStarted:()V:GetOnRewardedVideoStartedHandler:Android.Gms.Ads.Reward.IRewardedVideoAdListenerInvoker, Xamarin.GooglePlayServices.Ads.Lite\n" +
			"";
		mono.android.Runtime.register ("Youtube.Manager.Droid.Models.Ads.VideoAds.RewardedVideoAdListener, Youtube.Manager.Android", RewardedVideoAdListener.class, __md_methods);
	}


	public RewardedVideoAdListener ()
	{
		super ();
		if (getClass () == RewardedVideoAdListener.class)
			mono.android.TypeManager.Activate ("Youtube.Manager.Droid.Models.Ads.VideoAds.RewardedVideoAdListener, Youtube.Manager.Android", "", this, new java.lang.Object[] {  });
	}

	public RewardedVideoAdListener (com.google.android.gms.ads.reward.RewardedVideoAd p0)
	{
		super ();
		if (getClass () == RewardedVideoAdListener.class)
			mono.android.TypeManager.Activate ("Youtube.Manager.Droid.Models.Ads.VideoAds.RewardedVideoAdListener, Youtube.Manager.Android", "Android.Gms.Ads.Reward.IRewardedVideoAd, Xamarin.GooglePlayServices.Ads.Lite", this, new java.lang.Object[] { p0 });
	}


	public void onRewarded (com.google.android.gms.ads.reward.RewardItem p0)
	{
		n_onRewarded (p0);
	}

	private native void n_onRewarded (com.google.android.gms.ads.reward.RewardItem p0);


	public void onRewardedVideoAdClosed ()
	{
		n_onRewardedVideoAdClosed ();
	}

	private native void n_onRewardedVideoAdClosed ();


	public void onRewardedVideoAdFailedToLoad (int p0)
	{
		n_onRewardedVideoAdFailedToLoad (p0);
	}

	private native void n_onRewardedVideoAdFailedToLoad (int p0);


	public void onRewardedVideoAdLeftApplication ()
	{
		n_onRewardedVideoAdLeftApplication ();
	}

	private native void n_onRewardedVideoAdLeftApplication ();


	public void onRewardedVideoAdLoaded ()
	{
		n_onRewardedVideoAdLoaded ();
	}

	private native void n_onRewardedVideoAdLoaded ();


	public void onRewardedVideoAdOpened ()
	{
		n_onRewardedVideoAdOpened ();
	}

	private native void n_onRewardedVideoAdOpened ();


	public void onRewardedVideoStarted ()
	{
		n_onRewardedVideoStarted ();
	}

	private native void n_onRewardedVideoStarted ();

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

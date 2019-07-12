using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Google.YouTube.Player {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubePlayerSupportFragment']"
	[global::Android.Runtime.Register ("com/google/android/youtube/player/YouTubePlayerSupportFragment", DoNotGenerateAcw=true)]
	public partial class YouTubePlayerSupportFragment : global::Android.Support.V4.App.Fragment, global::Google.YouTube.Player.IYouTubePlayerProvider {

		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/google/android/youtube/player/YouTubePlayerSupportFragment", typeof (YouTubePlayerSupportFragment));
		internal static new IntPtr class_ref {
			get {
				return _members.JniPeerType.PeerReference.Handle;
			}
		}

		public override global::Java.Interop.JniPeerMembers JniPeerMembers {
			get { return _members; }
		}

		protected override IntPtr ThresholdClass {
			get { return _members.JniPeerType.PeerReference.Handle; }
		}

		protected override global::System.Type ThresholdType {
			get { return _members.ManagedPeerType; }
		}

		protected YouTubePlayerSupportFragment (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubePlayerSupportFragment']/constructor[@name='YouTubePlayerSupportFragment' and count(parameter)=0]"
		[Register (".ctor", "()V", "")]
		public unsafe YouTubePlayerSupportFragment ()
			: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			const string __id = "()V";

			if (((global::Java.Lang.Object) this).Handle != IntPtr.Zero)
				return;

			try {
				var __r = _members.InstanceMethods.StartCreateInstance (__id, ((object) this).GetType (), null);
				SetHandle (__r.Handle, JniHandleOwnership.TransferLocalRef);
				_members.InstanceMethods.FinishCreateInstance (__id, this, null);
			} finally {
			}
		}

		static Delegate cb_initialize_Ljava_lang_String_Lcom_google_android_youtube_player_YouTubePlayer_OnInitializedListener_;
#pragma warning disable 0169
		static Delegate GetInitialize_Ljava_lang_String_Lcom_google_android_youtube_player_YouTubePlayer_OnInitializedListener_Handler ()
		{
			if (cb_initialize_Ljava_lang_String_Lcom_google_android_youtube_player_YouTubePlayer_OnInitializedListener_ == null)
				cb_initialize_Ljava_lang_String_Lcom_google_android_youtube_player_YouTubePlayer_OnInitializedListener_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr, IntPtr>) n_Initialize_Ljava_lang_String_Lcom_google_android_youtube_player_YouTubePlayer_OnInitializedListener_);
			return cb_initialize_Ljava_lang_String_Lcom_google_android_youtube_player_YouTubePlayer_OnInitializedListener_;
		}

		static void n_Initialize_Ljava_lang_String_Lcom_google_android_youtube_player_YouTubePlayer_OnInitializedListener_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1)
		{
			global::Google.YouTube.Player.YouTubePlayerSupportFragment __this = global::Java.Lang.Object.GetObject<global::Google.YouTube.Player.YouTubePlayerSupportFragment> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			global::Google.YouTube.Player.IYouTubePlayerOnInitializedListener p1 = (global::Google.YouTube.Player.IYouTubePlayerOnInitializedListener)global::Java.Lang.Object.GetObject<global::Google.YouTube.Player.IYouTubePlayerOnInitializedListener> (native_p1, JniHandleOwnership.DoNotTransfer);
			__this.Initialize (p0, p1);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubePlayerSupportFragment']/method[@name='initialize' and count(parameter)=2 and parameter[1][@type='java.lang.String'] and parameter[2][@type='com.google.android.youtube.player.YouTubePlayer.OnInitializedListener']]"
		[Register ("initialize", "(Ljava/lang/String;Lcom/google/android/youtube/player/YouTubePlayer$OnInitializedListener;)V", "GetInitialize_Ljava_lang_String_Lcom_google_android_youtube_player_YouTubePlayer_OnInitializedListener_Handler")]
		public virtual unsafe void Initialize (string p0, global::Google.YouTube.Player.IYouTubePlayerOnInitializedListener p1)
		{
			const string __id = "initialize.(Ljava/lang/String;Lcom/google/android/youtube/player/YouTubePlayer$OnInitializedListener;)V";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [2];
				__args [0] = new JniArgumentValue (native_p0);
				__args [1] = new JniArgumentValue ((p1 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p1).Handle);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubePlayerSupportFragment']/method[@name='newInstance' and count(parameter)=0]"
		[Register ("newInstance", "()Lcom/google/android/youtube/player/YouTubePlayerSupportFragment;", "")]
		public static unsafe global::Google.YouTube.Player.YouTubePlayerSupportFragment NewInstance ()
		{
			const string __id = "newInstance.()Lcom/google/android/youtube/player/YouTubePlayerSupportFragment;";
			try {
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, null);
				return global::Java.Lang.Object.GetObject<global::Google.YouTube.Player.YouTubePlayerSupportFragment> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
			}
		}

	}
}

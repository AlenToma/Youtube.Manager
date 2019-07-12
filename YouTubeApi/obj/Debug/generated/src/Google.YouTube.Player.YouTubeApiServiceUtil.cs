using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Google.YouTube.Player {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeApiServiceUtil']"
	[global::Android.Runtime.Register ("com/google/android/youtube/player/YouTubeApiServiceUtil", DoNotGenerateAcw=true)]
	public sealed partial class YouTubeApiServiceUtil : global::Java.Lang.Object {

		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/google/android/youtube/player/YouTubeApiServiceUtil", typeof (YouTubeApiServiceUtil));
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

		internal YouTubeApiServiceUtil (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeApiServiceUtil']/method[@name='isYouTubeApiServiceAvailable' and count(parameter)=1 and parameter[1][@type='android.content.Context']]"
		[Register ("isYouTubeApiServiceAvailable", "(Landroid/content/Context;)Lcom/google/android/youtube/player/YouTubeInitializationResult;", "")]
		public static unsafe global::Google.YouTube.Player.YouTubeInitializationResult IsYouTubeApiServiceAvailable (global::Android.Content.Context p0)
		{
			const string __id = "isYouTubeApiServiceAvailable.(Landroid/content/Context;)Lcom/google/android/youtube/player/YouTubeInitializationResult;";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return global::Java.Lang.Object.GetObject<global::Google.YouTube.Player.YouTubeInitializationResult> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
			}
		}

	}
}

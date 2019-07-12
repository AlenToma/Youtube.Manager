using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Google.YouTube.Player {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeStandalonePlayer']"
	[global::Android.Runtime.Register ("com/google/android/youtube/player/YouTubeStandalonePlayer", DoNotGenerateAcw=true)]
	public sealed partial class YouTubeStandalonePlayer : global::Java.Lang.Object {

		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/google/android/youtube/player/YouTubeStandalonePlayer", typeof (YouTubeStandalonePlayer));
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

		internal YouTubeStandalonePlayer (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeStandalonePlayer']/method[@name='createPlaylistIntent' and count(parameter)=3 and parameter[1][@type='android.app.Activity'] and parameter[2][@type='java.lang.String'] and parameter[3][@type='java.lang.String']]"
		[Register ("createPlaylistIntent", "(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;", "")]
		public static unsafe global::Android.Content.Intent CreatePlaylistIntent (global::Android.App.Activity p0, string p1, string p2)
		{
			const string __id = "createPlaylistIntent.(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;";
			IntPtr native_p1 = JNIEnv.NewString (p1);
			IntPtr native_p2 = JNIEnv.NewString (p2);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [3];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				__args [1] = new JniArgumentValue (native_p1);
				__args [2] = new JniArgumentValue (native_p2);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return global::Java.Lang.Object.GetObject<global::Android.Content.Intent> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
				JNIEnv.DeleteLocalRef (native_p2);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeStandalonePlayer']/method[@name='createPlaylistIntent' and count(parameter)=7 and parameter[1][@type='android.app.Activity'] and parameter[2][@type='java.lang.String'] and parameter[3][@type='java.lang.String'] and parameter[4][@type='int'] and parameter[5][@type='int'] and parameter[6][@type='boolean'] and parameter[7][@type='boolean']]"
		[Register ("createPlaylistIntent", "(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/String;IIZZ)Landroid/content/Intent;", "")]
		public static unsafe global::Android.Content.Intent CreatePlaylistIntent (global::Android.App.Activity p0, string p1, string p2, int p3, int p4, bool p5, bool p6)
		{
			const string __id = "createPlaylistIntent.(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/String;IIZZ)Landroid/content/Intent;";
			IntPtr native_p1 = JNIEnv.NewString (p1);
			IntPtr native_p2 = JNIEnv.NewString (p2);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [7];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				__args [1] = new JniArgumentValue (native_p1);
				__args [2] = new JniArgumentValue (native_p2);
				__args [3] = new JniArgumentValue (p3);
				__args [4] = new JniArgumentValue (p4);
				__args [5] = new JniArgumentValue (p5);
				__args [6] = new JniArgumentValue (p6);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return global::Java.Lang.Object.GetObject<global::Android.Content.Intent> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
				JNIEnv.DeleteLocalRef (native_p2);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeStandalonePlayer']/method[@name='createVideoIntent' and count(parameter)=3 and parameter[1][@type='android.app.Activity'] and parameter[2][@type='java.lang.String'] and parameter[3][@type='java.lang.String']]"
		[Register ("createVideoIntent", "(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;", "")]
		public static unsafe global::Android.Content.Intent CreateVideoIntent (global::Android.App.Activity p0, string p1, string p2)
		{
			const string __id = "createVideoIntent.(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;";
			IntPtr native_p1 = JNIEnv.NewString (p1);
			IntPtr native_p2 = JNIEnv.NewString (p2);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [3];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				__args [1] = new JniArgumentValue (native_p1);
				__args [2] = new JniArgumentValue (native_p2);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return global::Java.Lang.Object.GetObject<global::Android.Content.Intent> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
				JNIEnv.DeleteLocalRef (native_p2);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeStandalonePlayer']/method[@name='createVideoIntent' and count(parameter)=6 and parameter[1][@type='android.app.Activity'] and parameter[2][@type='java.lang.String'] and parameter[3][@type='java.lang.String'] and parameter[4][@type='int'] and parameter[5][@type='boolean'] and parameter[6][@type='boolean']]"
		[Register ("createVideoIntent", "(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/String;IZZ)Landroid/content/Intent;", "")]
		public static unsafe global::Android.Content.Intent CreateVideoIntent (global::Android.App.Activity p0, string p1, string p2, int p3, bool p4, bool p5)
		{
			const string __id = "createVideoIntent.(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/String;IZZ)Landroid/content/Intent;";
			IntPtr native_p1 = JNIEnv.NewString (p1);
			IntPtr native_p2 = JNIEnv.NewString (p2);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [6];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				__args [1] = new JniArgumentValue (native_p1);
				__args [2] = new JniArgumentValue (native_p2);
				__args [3] = new JniArgumentValue (p3);
				__args [4] = new JniArgumentValue (p4);
				__args [5] = new JniArgumentValue (p5);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return global::Java.Lang.Object.GetObject<global::Android.Content.Intent> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
				JNIEnv.DeleteLocalRef (native_p2);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeStandalonePlayer']/method[@name='createVideosIntent' and count(parameter)=3 and parameter[1][@type='android.app.Activity'] and parameter[2][@type='java.lang.String'] and parameter[3][@type='java.util.List&lt;java.lang.String&gt;']]"
		[Register ("createVideosIntent", "(Landroid/app/Activity;Ljava/lang/String;Ljava/util/List;)Landroid/content/Intent;", "")]
		public static unsafe global::Android.Content.Intent CreateVideosIntent (global::Android.App.Activity p0, string p1, global::System.Collections.Generic.IList<string> p2)
		{
			const string __id = "createVideosIntent.(Landroid/app/Activity;Ljava/lang/String;Ljava/util/List;)Landroid/content/Intent;";
			IntPtr native_p1 = JNIEnv.NewString (p1);
			IntPtr native_p2 = global::Android.Runtime.JavaList<string>.ToLocalJniHandle (p2);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [3];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				__args [1] = new JniArgumentValue (native_p1);
				__args [2] = new JniArgumentValue (native_p2);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return global::Java.Lang.Object.GetObject<global::Android.Content.Intent> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
				JNIEnv.DeleteLocalRef (native_p2);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeStandalonePlayer']/method[@name='createVideosIntent' and count(parameter)=7 and parameter[1][@type='android.app.Activity'] and parameter[2][@type='java.lang.String'] and parameter[3][@type='java.util.List&lt;java.lang.String&gt;'] and parameter[4][@type='int'] and parameter[5][@type='int'] and parameter[6][@type='boolean'] and parameter[7][@type='boolean']]"
		[Register ("createVideosIntent", "(Landroid/app/Activity;Ljava/lang/String;Ljava/util/List;IIZZ)Landroid/content/Intent;", "")]
		public static unsafe global::Android.Content.Intent CreateVideosIntent (global::Android.App.Activity p0, string p1, global::System.Collections.Generic.IList<string> p2, int p3, int p4, bool p5, bool p6)
		{
			const string __id = "createVideosIntent.(Landroid/app/Activity;Ljava/lang/String;Ljava/util/List;IIZZ)Landroid/content/Intent;";
			IntPtr native_p1 = JNIEnv.NewString (p1);
			IntPtr native_p2 = global::Android.Runtime.JavaList<string>.ToLocalJniHandle (p2);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [7];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				__args [1] = new JniArgumentValue (native_p1);
				__args [2] = new JniArgumentValue (native_p2);
				__args [3] = new JniArgumentValue (p3);
				__args [4] = new JniArgumentValue (p4);
				__args [5] = new JniArgumentValue (p5);
				__args [6] = new JniArgumentValue (p6);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return global::Java.Lang.Object.GetObject<global::Android.Content.Intent> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
				JNIEnv.DeleteLocalRef (native_p2);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeStandalonePlayer']/method[@name='getReturnedInitializationResult' and count(parameter)=1 and parameter[1][@type='android.content.Intent']]"
		[Register ("getReturnedInitializationResult", "(Landroid/content/Intent;)Lcom/google/android/youtube/player/YouTubeInitializationResult;", "")]
		public static unsafe global::Google.YouTube.Player.YouTubeInitializationResult GetReturnedInitializationResult (global::Android.Content.Intent p0)
		{
			const string __id = "getReturnedInitializationResult.(Landroid/content/Intent;)Lcom/google/android/youtube/player/YouTubeInitializationResult;";
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

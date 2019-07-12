using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Google.YouTube.Player {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeIntents']"
	[global::Android.Runtime.Register ("com/google/android/youtube/player/YouTubeIntents", DoNotGenerateAcw=true)]
	public sealed partial class YouTubeIntents : global::Java.Lang.Object {

		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/google/android/youtube/player/YouTubeIntents", typeof (YouTubeIntents));
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

		internal YouTubeIntents (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeIntents']/method[@name='canResolveOpenPlaylistIntent' and count(parameter)=1 and parameter[1][@type='android.content.Context']]"
		[Register ("canResolveOpenPlaylistIntent", "(Landroid/content/Context;)Z", "")]
		public static unsafe bool CanResolveOpenPlaylistIntent (global::Android.Content.Context p0)
		{
			const string __id = "canResolveOpenPlaylistIntent.(Landroid/content/Context;)Z";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				var __rm = _members.StaticMethods.InvokeBooleanMethod (__id, __args);
				return __rm;
			} finally {
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeIntents']/method[@name='canResolvePlayPlaylistIntent' and count(parameter)=1 and parameter[1][@type='android.content.Context']]"
		[Register ("canResolvePlayPlaylistIntent", "(Landroid/content/Context;)Z", "")]
		public static unsafe bool CanResolvePlayPlaylistIntent (global::Android.Content.Context p0)
		{
			const string __id = "canResolvePlayPlaylistIntent.(Landroid/content/Context;)Z";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				var __rm = _members.StaticMethods.InvokeBooleanMethod (__id, __args);
				return __rm;
			} finally {
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeIntents']/method[@name='canResolvePlayVideoIntent' and count(parameter)=1 and parameter[1][@type='android.content.Context']]"
		[Register ("canResolvePlayVideoIntent", "(Landroid/content/Context;)Z", "")]
		public static unsafe bool CanResolvePlayVideoIntent (global::Android.Content.Context p0)
		{
			const string __id = "canResolvePlayVideoIntent.(Landroid/content/Context;)Z";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				var __rm = _members.StaticMethods.InvokeBooleanMethod (__id, __args);
				return __rm;
			} finally {
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeIntents']/method[@name='canResolvePlayVideoIntentWithOptions' and count(parameter)=1 and parameter[1][@type='android.content.Context']]"
		[Register ("canResolvePlayVideoIntentWithOptions", "(Landroid/content/Context;)Z", "")]
		public static unsafe bool CanResolvePlayVideoIntentWithOptions (global::Android.Content.Context p0)
		{
			const string __id = "canResolvePlayVideoIntentWithOptions.(Landroid/content/Context;)Z";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				var __rm = _members.StaticMethods.InvokeBooleanMethod (__id, __args);
				return __rm;
			} finally {
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeIntents']/method[@name='canResolveSearchIntent' and count(parameter)=1 and parameter[1][@type='android.content.Context']]"
		[Register ("canResolveSearchIntent", "(Landroid/content/Context;)Z", "")]
		public static unsafe bool CanResolveSearchIntent (global::Android.Content.Context p0)
		{
			const string __id = "canResolveSearchIntent.(Landroid/content/Context;)Z";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				var __rm = _members.StaticMethods.InvokeBooleanMethod (__id, __args);
				return __rm;
			} finally {
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeIntents']/method[@name='canResolveUploadIntent' and count(parameter)=1 and parameter[1][@type='android.content.Context']]"
		[Register ("canResolveUploadIntent", "(Landroid/content/Context;)Z", "")]
		public static unsafe bool CanResolveUploadIntent (global::Android.Content.Context p0)
		{
			const string __id = "canResolveUploadIntent.(Landroid/content/Context;)Z";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				var __rm = _members.StaticMethods.InvokeBooleanMethod (__id, __args);
				return __rm;
			} finally {
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeIntents']/method[@name='canResolveUserIntent' and count(parameter)=1 and parameter[1][@type='android.content.Context']]"
		[Register ("canResolveUserIntent", "(Landroid/content/Context;)Z", "")]
		public static unsafe bool CanResolveUserIntent (global::Android.Content.Context p0)
		{
			const string __id = "canResolveUserIntent.(Landroid/content/Context;)Z";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				var __rm = _members.StaticMethods.InvokeBooleanMethod (__id, __args);
				return __rm;
			} finally {
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeIntents']/method[@name='createOpenPlaylistIntent' and count(parameter)=2 and parameter[1][@type='android.content.Context'] and parameter[2][@type='java.lang.String']]"
		[Register ("createOpenPlaylistIntent", "(Landroid/content/Context;Ljava/lang/String;)Landroid/content/Intent;", "")]
		public static unsafe global::Android.Content.Intent CreateOpenPlaylistIntent (global::Android.Content.Context p0, string p1)
		{
			const string __id = "createOpenPlaylistIntent.(Landroid/content/Context;Ljava/lang/String;)Landroid/content/Intent;";
			IntPtr native_p1 = JNIEnv.NewString (p1);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [2];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				__args [1] = new JniArgumentValue (native_p1);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return global::Java.Lang.Object.GetObject<global::Android.Content.Intent> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeIntents']/method[@name='createPlayPlaylistIntent' and count(parameter)=2 and parameter[1][@type='android.content.Context'] and parameter[2][@type='java.lang.String']]"
		[Register ("createPlayPlaylistIntent", "(Landroid/content/Context;Ljava/lang/String;)Landroid/content/Intent;", "")]
		public static unsafe global::Android.Content.Intent CreatePlayPlaylistIntent (global::Android.Content.Context p0, string p1)
		{
			const string __id = "createPlayPlaylistIntent.(Landroid/content/Context;Ljava/lang/String;)Landroid/content/Intent;";
			IntPtr native_p1 = JNIEnv.NewString (p1);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [2];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				__args [1] = new JniArgumentValue (native_p1);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return global::Java.Lang.Object.GetObject<global::Android.Content.Intent> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeIntents']/method[@name='createPlayVideoIntent' and count(parameter)=2 and parameter[1][@type='android.content.Context'] and parameter[2][@type='java.lang.String']]"
		[Register ("createPlayVideoIntent", "(Landroid/content/Context;Ljava/lang/String;)Landroid/content/Intent;", "")]
		public static unsafe global::Android.Content.Intent CreatePlayVideoIntent (global::Android.Content.Context p0, string p1)
		{
			const string __id = "createPlayVideoIntent.(Landroid/content/Context;Ljava/lang/String;)Landroid/content/Intent;";
			IntPtr native_p1 = JNIEnv.NewString (p1);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [2];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				__args [1] = new JniArgumentValue (native_p1);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return global::Java.Lang.Object.GetObject<global::Android.Content.Intent> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeIntents']/method[@name='createPlayVideoIntentWithOptions' and count(parameter)=4 and parameter[1][@type='android.content.Context'] and parameter[2][@type='java.lang.String'] and parameter[3][@type='boolean'] and parameter[4][@type='boolean']]"
		[Register ("createPlayVideoIntentWithOptions", "(Landroid/content/Context;Ljava/lang/String;ZZ)Landroid/content/Intent;", "")]
		public static unsafe global::Android.Content.Intent CreatePlayVideoIntentWithOptions (global::Android.Content.Context p0, string p1, bool p2, bool p3)
		{
			const string __id = "createPlayVideoIntentWithOptions.(Landroid/content/Context;Ljava/lang/String;ZZ)Landroid/content/Intent;";
			IntPtr native_p1 = JNIEnv.NewString (p1);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [4];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				__args [1] = new JniArgumentValue (native_p1);
				__args [2] = new JniArgumentValue (p2);
				__args [3] = new JniArgumentValue (p3);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return global::Java.Lang.Object.GetObject<global::Android.Content.Intent> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeIntents']/method[@name='createSearchIntent' and count(parameter)=2 and parameter[1][@type='android.content.Context'] and parameter[2][@type='java.lang.String']]"
		[Register ("createSearchIntent", "(Landroid/content/Context;Ljava/lang/String;)Landroid/content/Intent;", "")]
		public static unsafe global::Android.Content.Intent CreateSearchIntent (global::Android.Content.Context p0, string p1)
		{
			const string __id = "createSearchIntent.(Landroid/content/Context;Ljava/lang/String;)Landroid/content/Intent;";
			IntPtr native_p1 = JNIEnv.NewString (p1);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [2];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				__args [1] = new JniArgumentValue (native_p1);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return global::Java.Lang.Object.GetObject<global::Android.Content.Intent> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeIntents']/method[@name='createUploadIntent' and count(parameter)=2 and parameter[1][@type='android.content.Context'] and parameter[2][@type='android.net.Uri']]"
		[Register ("createUploadIntent", "(Landroid/content/Context;Landroid/net/Uri;)Landroid/content/Intent;", "")]
		public static unsafe global::Android.Content.Intent CreateUploadIntent (global::Android.Content.Context p0, global::Android.Net.Uri p1)
		{
			const string __id = "createUploadIntent.(Landroid/content/Context;Landroid/net/Uri;)Landroid/content/Intent;";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [2];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				__args [1] = new JniArgumentValue ((p1 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p1).Handle);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return global::Java.Lang.Object.GetObject<global::Android.Content.Intent> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeIntents']/method[@name='createUserIntent' and count(parameter)=2 and parameter[1][@type='android.content.Context'] and parameter[2][@type='java.lang.String']]"
		[Register ("createUserIntent", "(Landroid/content/Context;Ljava/lang/String;)Landroid/content/Intent;", "")]
		public static unsafe global::Android.Content.Intent CreateUserIntent (global::Android.Content.Context p0, string p1)
		{
			const string __id = "createUserIntent.(Landroid/content/Context;Ljava/lang/String;)Landroid/content/Intent;";
			IntPtr native_p1 = JNIEnv.NewString (p1);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [2];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				__args [1] = new JniArgumentValue (native_p1);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return global::Java.Lang.Object.GetObject<global::Android.Content.Intent> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeIntents']/method[@name='getInstalledYouTubeVersionCode' and count(parameter)=1 and parameter[1][@type='android.content.Context']]"
		[Register ("getInstalledYouTubeVersionCode", "(Landroid/content/Context;)I", "")]
		public static unsafe int GetInstalledYouTubeVersionCode (global::Android.Content.Context p0)
		{
			const string __id = "getInstalledYouTubeVersionCode.(Landroid/content/Context;)I";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				var __rm = _members.StaticMethods.InvokeInt32Method (__id, __args);
				return __rm;
			} finally {
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeIntents']/method[@name='getInstalledYouTubeVersionName' and count(parameter)=1 and parameter[1][@type='android.content.Context']]"
		[Register ("getInstalledYouTubeVersionName", "(Landroid/content/Context;)Ljava/lang/String;", "")]
		public static unsafe string GetInstalledYouTubeVersionName (global::Android.Content.Context p0)
		{
			const string __id = "getInstalledYouTubeVersionName.(Landroid/content/Context;)Ljava/lang/String;";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return JNIEnv.GetString (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeIntents']/method[@name='isYouTubeInstalled' and count(parameter)=1 and parameter[1][@type='android.content.Context']]"
		[Register ("isYouTubeInstalled", "(Landroid/content/Context;)Z", "")]
		public static unsafe bool IsYouTubeInstalled (global::Android.Content.Context p0)
		{
			const string __id = "isYouTubeInstalled.(Landroid/content/Context;)Z";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				var __rm = _members.StaticMethods.InvokeBooleanMethod (__id, __args);
				return __rm;
			} finally {
			}
		}

	}
}

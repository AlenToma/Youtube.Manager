using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Google.YouTube.Player {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeThumbnailLoader.ErrorReason']"
	[global::Android.Runtime.Register ("com/google/android/youtube/player/YouTubeThumbnailLoader$ErrorReason", DoNotGenerateAcw=true)]
	public sealed partial class YouTubeThumbnailLoaderErrorReason : global::Java.Lang.Enum {



		// Metadata.xml XPath field reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeThumbnailLoader.ErrorReason']/field[@name='INTERNAL_ERROR']"
		[Register ("INTERNAL_ERROR")]
		public static global::Google.YouTube.Player.YouTubeThumbnailLoaderErrorReason InternalError {
			get {
				const string __id = "INTERNAL_ERROR.Lcom/google/android/youtube/player/YouTubeThumbnailLoader$ErrorReason;";

				var __v = _members.StaticFields.GetObjectValue (__id);
				return global::Java.Lang.Object.GetObject<global::Google.YouTube.Player.YouTubeThumbnailLoaderErrorReason> (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeThumbnailLoader.ErrorReason']/field[@name='NETWORK_ERROR']"
		[Register ("NETWORK_ERROR")]
		public static global::Google.YouTube.Player.YouTubeThumbnailLoaderErrorReason NetworkError {
			get {
				const string __id = "NETWORK_ERROR.Lcom/google/android/youtube/player/YouTubeThumbnailLoader$ErrorReason;";

				var __v = _members.StaticFields.GetObjectValue (__id);
				return global::Java.Lang.Object.GetObject<global::Google.YouTube.Player.YouTubeThumbnailLoaderErrorReason> (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeThumbnailLoader.ErrorReason']/field[@name='UNKNOWN']"
		[Register ("UNKNOWN")]
		public static global::Google.YouTube.Player.YouTubeThumbnailLoaderErrorReason Unknown {
			get {
				const string __id = "UNKNOWN.Lcom/google/android/youtube/player/YouTubeThumbnailLoader$ErrorReason;";

				var __v = _members.StaticFields.GetObjectValue (__id);
				return global::Java.Lang.Object.GetObject<global::Google.YouTube.Player.YouTubeThumbnailLoaderErrorReason> (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
		}
		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/google/android/youtube/player/YouTubeThumbnailLoader$ErrorReason", typeof (YouTubeThumbnailLoaderErrorReason));
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

		internal YouTubeThumbnailLoaderErrorReason (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeThumbnailLoader.ErrorReason']/method[@name='valueOf' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("valueOf", "(Ljava/lang/String;)Lcom/google/android/youtube/player/YouTubeThumbnailLoader$ErrorReason;", "")]
		public static unsafe global::Google.YouTube.Player.YouTubeThumbnailLoaderErrorReason ValueOf (string p0)
		{
			const string __id = "valueOf.(Ljava/lang/String;)Lcom/google/android/youtube/player/YouTubeThumbnailLoader$ErrorReason;";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return global::Java.Lang.Object.GetObject<global::Google.YouTube.Player.YouTubeThumbnailLoaderErrorReason> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/class[@name='YouTubeThumbnailLoader.ErrorReason']/method[@name='values' and count(parameter)=0]"
		[Register ("values", "()[Lcom/google/android/youtube/player/YouTubeThumbnailLoader$ErrorReason;", "")]
		public static unsafe global::Google.YouTube.Player.YouTubeThumbnailLoaderErrorReason[] Values ()
		{
			const string __id = "values.()[Lcom/google/android/youtube/player/YouTubeThumbnailLoader$ErrorReason;";
			try {
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, null);
				return (global::Google.YouTube.Player.YouTubeThumbnailLoaderErrorReason[]) JNIEnv.GetArray (__rm.Handle, JniHandleOwnership.TransferLocalRef, typeof (global::Google.YouTube.Player.YouTubeThumbnailLoaderErrorReason));
			} finally {
			}
		}

	}

	// Metadata.xml XPath interface reference: path="/api/package[@name='com.google.android.youtube.player']/interface[@name='YouTubeThumbnailLoader.OnThumbnailLoadedListener']"
	[Register ("com/google/android/youtube/player/YouTubeThumbnailLoader$OnThumbnailLoadedListener", "", "Google.YouTube.Player.IYouTubeThumbnailLoaderOnThumbnailLoadedListenerInvoker")]
	public partial interface IYouTubeThumbnailLoaderOnThumbnailLoadedListener : IJavaObject {

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/interface[@name='YouTubeThumbnailLoader.OnThumbnailLoadedListener']/method[@name='onThumbnailError' and count(parameter)=2 and parameter[1][@type='com.google.android.youtube.player.YouTubeThumbnailView'] and parameter[2][@type='com.google.android.youtube.player.YouTubeThumbnailLoader.ErrorReason']]"
		[Register ("onThumbnailError", "(Lcom/google/android/youtube/player/YouTubeThumbnailView;Lcom/google/android/youtube/player/YouTubeThumbnailLoader$ErrorReason;)V", "GetOnThumbnailError_Lcom_google_android_youtube_player_YouTubeThumbnailView_Lcom_google_android_youtube_player_YouTubeThumbnailLoader_ErrorReason_Handler:Google.YouTube.Player.IYouTubeThumbnailLoaderOnThumbnailLoadedListenerInvoker, YouTubeApi")]
		void OnThumbnailError (global::Google.YouTube.Player.YouTubeThumbnailView p0, global::Google.YouTube.Player.YouTubeThumbnailLoaderErrorReason p1);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/interface[@name='YouTubeThumbnailLoader.OnThumbnailLoadedListener']/method[@name='onThumbnailLoaded' and count(parameter)=2 and parameter[1][@type='com.google.android.youtube.player.YouTubeThumbnailView'] and parameter[2][@type='java.lang.String']]"
		[Register ("onThumbnailLoaded", "(Lcom/google/android/youtube/player/YouTubeThumbnailView;Ljava/lang/String;)V", "GetOnThumbnailLoaded_Lcom_google_android_youtube_player_YouTubeThumbnailView_Ljava_lang_String_Handler:Google.YouTube.Player.IYouTubeThumbnailLoaderOnThumbnailLoadedListenerInvoker, YouTubeApi")]
		void OnThumbnailLoaded (global::Google.YouTube.Player.YouTubeThumbnailView p0, string p1);

	}

	[global::Android.Runtime.Register ("com/google/android/youtube/player/YouTubeThumbnailLoader$OnThumbnailLoadedListener", DoNotGenerateAcw=true)]
	internal class IYouTubeThumbnailLoaderOnThumbnailLoadedListenerInvoker : global::Java.Lang.Object, IYouTubeThumbnailLoaderOnThumbnailLoadedListener {

		internal    new     static  readonly    JniPeerMembers  _members    = new JniPeerMembers ("com/google/android/youtube/player/YouTubeThumbnailLoader$OnThumbnailLoadedListener", typeof (IYouTubeThumbnailLoaderOnThumbnailLoadedListenerInvoker));

		static IntPtr java_class_ref {
			get { return _members.JniPeerType.PeerReference.Handle; }
		}

		public override global::Java.Interop.JniPeerMembers JniPeerMembers {
			get { return _members; }
		}

		protected override IntPtr ThresholdClass {
			get { return class_ref; }
		}

		protected override global::System.Type ThresholdType {
			get { return _members.ManagedPeerType; }
		}

		IntPtr class_ref;

		public static IYouTubeThumbnailLoaderOnThumbnailLoadedListener GetObject (IntPtr handle, JniHandleOwnership transfer)
		{
			return global::Java.Lang.Object.GetObject<IYouTubeThumbnailLoaderOnThumbnailLoadedListener> (handle, transfer);
		}

		static IntPtr Validate (IntPtr handle)
		{
			if (!JNIEnv.IsInstanceOf (handle, java_class_ref))
				throw new InvalidCastException (string.Format ("Unable to convert instance of type '{0}' to type '{1}'.",
							JNIEnv.GetClassNameFromInstance (handle), "com.google.android.youtube.player.YouTubeThumbnailLoader.OnThumbnailLoadedListener"));
			return handle;
		}

		protected override void Dispose (bool disposing)
		{
			if (this.class_ref != IntPtr.Zero)
				JNIEnv.DeleteGlobalRef (this.class_ref);
			this.class_ref = IntPtr.Zero;
			base.Dispose (disposing);
		}

		public IYouTubeThumbnailLoaderOnThumbnailLoadedListenerInvoker (IntPtr handle, JniHandleOwnership transfer) : base (Validate (handle), transfer)
		{
			IntPtr local_ref = JNIEnv.GetObjectClass (((global::Java.Lang.Object) this).Handle);
			this.class_ref = JNIEnv.NewGlobalRef (local_ref);
			JNIEnv.DeleteLocalRef (local_ref);
		}

		static Delegate cb_onThumbnailError_Lcom_google_android_youtube_player_YouTubeThumbnailView_Lcom_google_android_youtube_player_YouTubeThumbnailLoader_ErrorReason_;
#pragma warning disable 0169
		static Delegate GetOnThumbnailError_Lcom_google_android_youtube_player_YouTubeThumbnailView_Lcom_google_android_youtube_player_YouTubeThumbnailLoader_ErrorReason_Handler ()
		{
			if (cb_onThumbnailError_Lcom_google_android_youtube_player_YouTubeThumbnailView_Lcom_google_android_youtube_player_YouTubeThumbnailLoader_ErrorReason_ == null)
				cb_onThumbnailError_Lcom_google_android_youtube_player_YouTubeThumbnailView_Lcom_google_android_youtube_player_YouTubeThumbnailLoader_ErrorReason_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr, IntPtr>) n_OnThumbnailError_Lcom_google_android_youtube_player_YouTubeThumbnailView_Lcom_google_android_youtube_player_YouTubeThumbnailLoader_ErrorReason_);
			return cb_onThumbnailError_Lcom_google_android_youtube_player_YouTubeThumbnailView_Lcom_google_android_youtube_player_YouTubeThumbnailLoader_ErrorReason_;
		}

		static void n_OnThumbnailError_Lcom_google_android_youtube_player_YouTubeThumbnailView_Lcom_google_android_youtube_player_YouTubeThumbnailLoader_ErrorReason_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1)
		{
			global::Google.YouTube.Player.IYouTubeThumbnailLoaderOnThumbnailLoadedListener __this = global::Java.Lang.Object.GetObject<global::Google.YouTube.Player.IYouTubeThumbnailLoaderOnThumbnailLoadedListener> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Google.YouTube.Player.YouTubeThumbnailView p0 = global::Java.Lang.Object.GetObject<global::Google.YouTube.Player.YouTubeThumbnailView> (native_p0, JniHandleOwnership.DoNotTransfer);
			global::Google.YouTube.Player.YouTubeThumbnailLoaderErrorReason p1 = global::Java.Lang.Object.GetObject<global::Google.YouTube.Player.YouTubeThumbnailLoaderErrorReason> (native_p1, JniHandleOwnership.DoNotTransfer);
			__this.OnThumbnailError (p0, p1);
		}
#pragma warning restore 0169

		IntPtr id_onThumbnailError_Lcom_google_android_youtube_player_YouTubeThumbnailView_Lcom_google_android_youtube_player_YouTubeThumbnailLoader_ErrorReason_;
		public unsafe void OnThumbnailError (global::Google.YouTube.Player.YouTubeThumbnailView p0, global::Google.YouTube.Player.YouTubeThumbnailLoaderErrorReason p1)
		{
			if (id_onThumbnailError_Lcom_google_android_youtube_player_YouTubeThumbnailView_Lcom_google_android_youtube_player_YouTubeThumbnailLoader_ErrorReason_ == IntPtr.Zero)
				id_onThumbnailError_Lcom_google_android_youtube_player_YouTubeThumbnailView_Lcom_google_android_youtube_player_YouTubeThumbnailLoader_ErrorReason_ = JNIEnv.GetMethodID (class_ref, "onThumbnailError", "(Lcom/google/android/youtube/player/YouTubeThumbnailView;Lcom/google/android/youtube/player/YouTubeThumbnailLoader$ErrorReason;)V");
			JValue* __args = stackalloc JValue [2];
			__args [0] = new JValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
			__args [1] = new JValue ((p1 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p1).Handle);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_onThumbnailError_Lcom_google_android_youtube_player_YouTubeThumbnailView_Lcom_google_android_youtube_player_YouTubeThumbnailLoader_ErrorReason_, __args);
		}

		static Delegate cb_onThumbnailLoaded_Lcom_google_android_youtube_player_YouTubeThumbnailView_Ljava_lang_String_;
#pragma warning disable 0169
		static Delegate GetOnThumbnailLoaded_Lcom_google_android_youtube_player_YouTubeThumbnailView_Ljava_lang_String_Handler ()
		{
			if (cb_onThumbnailLoaded_Lcom_google_android_youtube_player_YouTubeThumbnailView_Ljava_lang_String_ == null)
				cb_onThumbnailLoaded_Lcom_google_android_youtube_player_YouTubeThumbnailView_Ljava_lang_String_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr, IntPtr>) n_OnThumbnailLoaded_Lcom_google_android_youtube_player_YouTubeThumbnailView_Ljava_lang_String_);
			return cb_onThumbnailLoaded_Lcom_google_android_youtube_player_YouTubeThumbnailView_Ljava_lang_String_;
		}

		static void n_OnThumbnailLoaded_Lcom_google_android_youtube_player_YouTubeThumbnailView_Ljava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1)
		{
			global::Google.YouTube.Player.IYouTubeThumbnailLoaderOnThumbnailLoadedListener __this = global::Java.Lang.Object.GetObject<global::Google.YouTube.Player.IYouTubeThumbnailLoaderOnThumbnailLoadedListener> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Google.YouTube.Player.YouTubeThumbnailView p0 = global::Java.Lang.Object.GetObject<global::Google.YouTube.Player.YouTubeThumbnailView> (native_p0, JniHandleOwnership.DoNotTransfer);
			string p1 = JNIEnv.GetString (native_p1, JniHandleOwnership.DoNotTransfer);
			__this.OnThumbnailLoaded (p0, p1);
		}
#pragma warning restore 0169

		IntPtr id_onThumbnailLoaded_Lcom_google_android_youtube_player_YouTubeThumbnailView_Ljava_lang_String_;
		public unsafe void OnThumbnailLoaded (global::Google.YouTube.Player.YouTubeThumbnailView p0, string p1)
		{
			if (id_onThumbnailLoaded_Lcom_google_android_youtube_player_YouTubeThumbnailView_Ljava_lang_String_ == IntPtr.Zero)
				id_onThumbnailLoaded_Lcom_google_android_youtube_player_YouTubeThumbnailView_Ljava_lang_String_ = JNIEnv.GetMethodID (class_ref, "onThumbnailLoaded", "(Lcom/google/android/youtube/player/YouTubeThumbnailView;Ljava/lang/String;)V");
			IntPtr native_p1 = JNIEnv.NewString (p1);
			JValue* __args = stackalloc JValue [2];
			__args [0] = new JValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
			__args [1] = new JValue (native_p1);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_onThumbnailLoaded_Lcom_google_android_youtube_player_YouTubeThumbnailView_Ljava_lang_String_, __args);
			JNIEnv.DeleteLocalRef (native_p1);
		}

	}

	// event args for com.google.android.youtube.player.YouTubeThumbnailLoader.OnThumbnailLoadedListener.onThumbnailError
	public partial class ThumbnailErrorEventArgs : global::System.EventArgs {

		public ThumbnailErrorEventArgs (global::Google.YouTube.Player.YouTubeThumbnailView p0, global::Google.YouTube.Player.YouTubeThumbnailLoaderErrorReason p1)
		{
			this.p0 = p0;
			this.p1 = p1;
		}

		global::Google.YouTube.Player.YouTubeThumbnailView p0;
		public global::Google.YouTube.Player.YouTubeThumbnailView P0 {
			get { return p0; }
		}

		global::Google.YouTube.Player.YouTubeThumbnailLoaderErrorReason p1;
		public global::Google.YouTube.Player.YouTubeThumbnailLoaderErrorReason P1 {
			get { return p1; }
		}
	}

	// event args for com.google.android.youtube.player.YouTubeThumbnailLoader.OnThumbnailLoadedListener.onThumbnailLoaded
	public partial class ThumbnailLoadedEventArgs : global::System.EventArgs {

		public ThumbnailLoadedEventArgs (global::Google.YouTube.Player.YouTubeThumbnailView p0, string p1)
		{
			this.p0 = p0;
			this.p1 = p1;
		}

		global::Google.YouTube.Player.YouTubeThumbnailView p0;
		public global::Google.YouTube.Player.YouTubeThumbnailView P0 {
			get { return p0; }
		}

		string p1;
		public string P1 {
			get { return p1; }
		}
	}

	[global::Android.Runtime.Register ("mono/com/google/android/youtube/player/YouTubeThumbnailLoader_OnThumbnailLoadedListenerImplementor")]
	internal sealed partial class IYouTubeThumbnailLoaderOnThumbnailLoadedListenerImplementor : global::Java.Lang.Object, IYouTubeThumbnailLoaderOnThumbnailLoadedListener {

		object sender;

		public IYouTubeThumbnailLoaderOnThumbnailLoadedListenerImplementor (object sender)
			: base (
				global::Android.Runtime.JNIEnv.StartCreateInstance ("mono/com/google/android/youtube/player/YouTubeThumbnailLoader_OnThumbnailLoadedListenerImplementor", "()V"),
				JniHandleOwnership.TransferLocalRef)
		{
			global::Android.Runtime.JNIEnv.FinishCreateInstance (((global::Java.Lang.Object) this).Handle, "()V");
			this.sender = sender;
		}

#pragma warning disable 0649
		public EventHandler<ThumbnailErrorEventArgs> OnThumbnailErrorHandler;
#pragma warning restore 0649

		public void OnThumbnailError (global::Google.YouTube.Player.YouTubeThumbnailView p0, global::Google.YouTube.Player.YouTubeThumbnailLoaderErrorReason p1)
		{
			var __h = OnThumbnailErrorHandler;
			if (__h != null)
				__h (sender, new ThumbnailErrorEventArgs (p0, p1));
		}
#pragma warning disable 0649
		public EventHandler<ThumbnailLoadedEventArgs> OnThumbnailLoadedHandler;
#pragma warning restore 0649

		public void OnThumbnailLoaded (global::Google.YouTube.Player.YouTubeThumbnailView p0, string p1)
		{
			var __h = OnThumbnailLoadedHandler;
			if (__h != null)
				__h (sender, new ThumbnailLoadedEventArgs (p0, p1));
		}

		internal static bool __IsEmpty (IYouTubeThumbnailLoaderOnThumbnailLoadedListenerImplementor value)
		{
			return value.OnThumbnailErrorHandler == null && value.OnThumbnailLoadedHandler == null;
		}
	}


	// Metadata.xml XPath interface reference: path="/api/package[@name='com.google.android.youtube.player']/interface[@name='YouTubeThumbnailLoader']"
	[Register ("com/google/android/youtube/player/YouTubeThumbnailLoader", "", "Google.YouTube.Player.IYouTubeThumbnailLoaderInvoker")]
	public partial interface IYouTubeThumbnailLoader : IJavaObject {

		bool HasNext {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/interface[@name='YouTubeThumbnailLoader']/method[@name='hasNext' and count(parameter)=0]"
			[Register ("hasNext", "()Z", "GetHasNextHandler:Google.YouTube.Player.IYouTubeThumbnailLoaderInvoker, YouTubeApi")] get;
		}

		bool HasPrevious {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/interface[@name='YouTubeThumbnailLoader']/method[@name='hasPrevious' and count(parameter)=0]"
			[Register ("hasPrevious", "()Z", "GetHasPreviousHandler:Google.YouTube.Player.IYouTubeThumbnailLoaderInvoker, YouTubeApi")] get;
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/interface[@name='YouTubeThumbnailLoader']/method[@name='first' and count(parameter)=0]"
		[Register ("first", "()V", "GetFirstHandler:Google.YouTube.Player.IYouTubeThumbnailLoaderInvoker, YouTubeApi")]
		void First ();

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/interface[@name='YouTubeThumbnailLoader']/method[@name='next' and count(parameter)=0]"
		[Register ("next", "()V", "GetNextHandler:Google.YouTube.Player.IYouTubeThumbnailLoaderInvoker, YouTubeApi")]
		void Next ();

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/interface[@name='YouTubeThumbnailLoader']/method[@name='previous' and count(parameter)=0]"
		[Register ("previous", "()V", "GetPreviousHandler:Google.YouTube.Player.IYouTubeThumbnailLoaderInvoker, YouTubeApi")]
		void Previous ();

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/interface[@name='YouTubeThumbnailLoader']/method[@name='release' and count(parameter)=0]"
		[Register ("release", "()V", "GetReleaseHandler:Google.YouTube.Player.IYouTubeThumbnailLoaderInvoker, YouTubeApi")]
		void Release ();

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/interface[@name='YouTubeThumbnailLoader']/method[@name='setOnThumbnailLoadedListener' and count(parameter)=1 and parameter[1][@type='com.google.android.youtube.player.YouTubeThumbnailLoader.OnThumbnailLoadedListener']]"
		[Register ("setOnThumbnailLoadedListener", "(Lcom/google/android/youtube/player/YouTubeThumbnailLoader$OnThumbnailLoadedListener;)V", "GetSetOnThumbnailLoadedListener_Lcom_google_android_youtube_player_YouTubeThumbnailLoader_OnThumbnailLoadedListener_Handler:Google.YouTube.Player.IYouTubeThumbnailLoaderInvoker, YouTubeApi")]
		void SetOnThumbnailLoadedListener (global::Google.YouTube.Player.IYouTubeThumbnailLoaderOnThumbnailLoadedListener p0);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/interface[@name='YouTubeThumbnailLoader']/method[@name='setPlaylist' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("setPlaylist", "(Ljava/lang/String;)V", "GetSetPlaylist_Ljava_lang_String_Handler:Google.YouTube.Player.IYouTubeThumbnailLoaderInvoker, YouTubeApi")]
		void SetPlaylist (string p0);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/interface[@name='YouTubeThumbnailLoader']/method[@name='setPlaylist' and count(parameter)=2 and parameter[1][@type='java.lang.String'] and parameter[2][@type='int']]"
		[Register ("setPlaylist", "(Ljava/lang/String;I)V", "GetSetPlaylist_Ljava_lang_String_IHandler:Google.YouTube.Player.IYouTubeThumbnailLoaderInvoker, YouTubeApi")]
		void SetPlaylist (string p0, int p1);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.google.android.youtube.player']/interface[@name='YouTubeThumbnailLoader']/method[@name='setVideo' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("setVideo", "(Ljava/lang/String;)V", "GetSetVideo_Ljava_lang_String_Handler:Google.YouTube.Player.IYouTubeThumbnailLoaderInvoker, YouTubeApi")]
		void SetVideo (string p0);

	}

	[global::Android.Runtime.Register ("com/google/android/youtube/player/YouTubeThumbnailLoader", DoNotGenerateAcw=true)]
	internal class IYouTubeThumbnailLoaderInvoker : global::Java.Lang.Object, IYouTubeThumbnailLoader {

		internal    new     static  readonly    JniPeerMembers  _members    = new JniPeerMembers ("com/google/android/youtube/player/YouTubeThumbnailLoader", typeof (IYouTubeThumbnailLoaderInvoker));

		static IntPtr java_class_ref {
			get { return _members.JniPeerType.PeerReference.Handle; }
		}

		public override global::Java.Interop.JniPeerMembers JniPeerMembers {
			get { return _members; }
		}

		protected override IntPtr ThresholdClass {
			get { return class_ref; }
		}

		protected override global::System.Type ThresholdType {
			get { return _members.ManagedPeerType; }
		}

		IntPtr class_ref;

		public static IYouTubeThumbnailLoader GetObject (IntPtr handle, JniHandleOwnership transfer)
		{
			return global::Java.Lang.Object.GetObject<IYouTubeThumbnailLoader> (handle, transfer);
		}

		static IntPtr Validate (IntPtr handle)
		{
			if (!JNIEnv.IsInstanceOf (handle, java_class_ref))
				throw new InvalidCastException (string.Format ("Unable to convert instance of type '{0}' to type '{1}'.",
							JNIEnv.GetClassNameFromInstance (handle), "com.google.android.youtube.player.YouTubeThumbnailLoader"));
			return handle;
		}

		protected override void Dispose (bool disposing)
		{
			if (this.class_ref != IntPtr.Zero)
				JNIEnv.DeleteGlobalRef (this.class_ref);
			this.class_ref = IntPtr.Zero;
			base.Dispose (disposing);
		}

		public IYouTubeThumbnailLoaderInvoker (IntPtr handle, JniHandleOwnership transfer) : base (Validate (handle), transfer)
		{
			IntPtr local_ref = JNIEnv.GetObjectClass (((global::Java.Lang.Object) this).Handle);
			this.class_ref = JNIEnv.NewGlobalRef (local_ref);
			JNIEnv.DeleteLocalRef (local_ref);
		}

		static Delegate cb_hasNext;
#pragma warning disable 0169
		static Delegate GetHasNextHandler ()
		{
			if (cb_hasNext == null)
				cb_hasNext = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, bool>) n_HasNext);
			return cb_hasNext;
		}

		static bool n_HasNext (IntPtr jnienv, IntPtr native__this)
		{
			global::Google.YouTube.Player.IYouTubeThumbnailLoader __this = global::Java.Lang.Object.GetObject<global::Google.YouTube.Player.IYouTubeThumbnailLoader> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return __this.HasNext;
		}
#pragma warning restore 0169

		IntPtr id_hasNext;
		public unsafe bool HasNext {
			get {
				if (id_hasNext == IntPtr.Zero)
					id_hasNext = JNIEnv.GetMethodID (class_ref, "hasNext", "()Z");
				return JNIEnv.CallBooleanMethod (((global::Java.Lang.Object) this).Handle, id_hasNext);
			}
		}

		static Delegate cb_hasPrevious;
#pragma warning disable 0169
		static Delegate GetHasPreviousHandler ()
		{
			if (cb_hasPrevious == null)
				cb_hasPrevious = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, bool>) n_HasPrevious);
			return cb_hasPrevious;
		}

		static bool n_HasPrevious (IntPtr jnienv, IntPtr native__this)
		{
			global::Google.YouTube.Player.IYouTubeThumbnailLoader __this = global::Java.Lang.Object.GetObject<global::Google.YouTube.Player.IYouTubeThumbnailLoader> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return __this.HasPrevious;
		}
#pragma warning restore 0169

		IntPtr id_hasPrevious;
		public unsafe bool HasPrevious {
			get {
				if (id_hasPrevious == IntPtr.Zero)
					id_hasPrevious = JNIEnv.GetMethodID (class_ref, "hasPrevious", "()Z");
				return JNIEnv.CallBooleanMethod (((global::Java.Lang.Object) this).Handle, id_hasPrevious);
			}
		}

		static Delegate cb_first;
#pragma warning disable 0169
		static Delegate GetFirstHandler ()
		{
			if (cb_first == null)
				cb_first = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr>) n_First);
			return cb_first;
		}

		static void n_First (IntPtr jnienv, IntPtr native__this)
		{
			global::Google.YouTube.Player.IYouTubeThumbnailLoader __this = global::Java.Lang.Object.GetObject<global::Google.YouTube.Player.IYouTubeThumbnailLoader> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.First ();
		}
#pragma warning restore 0169

		IntPtr id_first;
		public unsafe void First ()
		{
			if (id_first == IntPtr.Zero)
				id_first = JNIEnv.GetMethodID (class_ref, "first", "()V");
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_first);
		}

		static Delegate cb_next;
#pragma warning disable 0169
		static Delegate GetNextHandler ()
		{
			if (cb_next == null)
				cb_next = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr>) n_Next);
			return cb_next;
		}

		static void n_Next (IntPtr jnienv, IntPtr native__this)
		{
			global::Google.YouTube.Player.IYouTubeThumbnailLoader __this = global::Java.Lang.Object.GetObject<global::Google.YouTube.Player.IYouTubeThumbnailLoader> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.Next ();
		}
#pragma warning restore 0169

		IntPtr id_next;
		public unsafe void Next ()
		{
			if (id_next == IntPtr.Zero)
				id_next = JNIEnv.GetMethodID (class_ref, "next", "()V");
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_next);
		}

		static Delegate cb_previous;
#pragma warning disable 0169
		static Delegate GetPreviousHandler ()
		{
			if (cb_previous == null)
				cb_previous = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr>) n_Previous);
			return cb_previous;
		}

		static void n_Previous (IntPtr jnienv, IntPtr native__this)
		{
			global::Google.YouTube.Player.IYouTubeThumbnailLoader __this = global::Java.Lang.Object.GetObject<global::Google.YouTube.Player.IYouTubeThumbnailLoader> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.Previous ();
		}
#pragma warning restore 0169

		IntPtr id_previous;
		public unsafe void Previous ()
		{
			if (id_previous == IntPtr.Zero)
				id_previous = JNIEnv.GetMethodID (class_ref, "previous", "()V");
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_previous);
		}

		static Delegate cb_release;
#pragma warning disable 0169
		static Delegate GetReleaseHandler ()
		{
			if (cb_release == null)
				cb_release = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr>) n_Release);
			return cb_release;
		}

		static void n_Release (IntPtr jnienv, IntPtr native__this)
		{
			global::Google.YouTube.Player.IYouTubeThumbnailLoader __this = global::Java.Lang.Object.GetObject<global::Google.YouTube.Player.IYouTubeThumbnailLoader> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.Release ();
		}
#pragma warning restore 0169

		IntPtr id_release;
		public unsafe void Release ()
		{
			if (id_release == IntPtr.Zero)
				id_release = JNIEnv.GetMethodID (class_ref, "release", "()V");
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_release);
		}

		static Delegate cb_setOnThumbnailLoadedListener_Lcom_google_android_youtube_player_YouTubeThumbnailLoader_OnThumbnailLoadedListener_;
#pragma warning disable 0169
		static Delegate GetSetOnThumbnailLoadedListener_Lcom_google_android_youtube_player_YouTubeThumbnailLoader_OnThumbnailLoadedListener_Handler ()
		{
			if (cb_setOnThumbnailLoadedListener_Lcom_google_android_youtube_player_YouTubeThumbnailLoader_OnThumbnailLoadedListener_ == null)
				cb_setOnThumbnailLoadedListener_Lcom_google_android_youtube_player_YouTubeThumbnailLoader_OnThumbnailLoadedListener_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr>) n_SetOnThumbnailLoadedListener_Lcom_google_android_youtube_player_YouTubeThumbnailLoader_OnThumbnailLoadedListener_);
			return cb_setOnThumbnailLoadedListener_Lcom_google_android_youtube_player_YouTubeThumbnailLoader_OnThumbnailLoadedListener_;
		}

		static void n_SetOnThumbnailLoadedListener_Lcom_google_android_youtube_player_YouTubeThumbnailLoader_OnThumbnailLoadedListener_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Google.YouTube.Player.IYouTubeThumbnailLoader __this = global::Java.Lang.Object.GetObject<global::Google.YouTube.Player.IYouTubeThumbnailLoader> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Google.YouTube.Player.IYouTubeThumbnailLoaderOnThumbnailLoadedListener p0 = (global::Google.YouTube.Player.IYouTubeThumbnailLoaderOnThumbnailLoadedListener)global::Java.Lang.Object.GetObject<global::Google.YouTube.Player.IYouTubeThumbnailLoaderOnThumbnailLoadedListener> (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.SetOnThumbnailLoadedListener (p0);
		}
#pragma warning restore 0169

		IntPtr id_setOnThumbnailLoadedListener_Lcom_google_android_youtube_player_YouTubeThumbnailLoader_OnThumbnailLoadedListener_;
		public unsafe void SetOnThumbnailLoadedListener (global::Google.YouTube.Player.IYouTubeThumbnailLoaderOnThumbnailLoadedListener p0)
		{
			if (id_setOnThumbnailLoadedListener_Lcom_google_android_youtube_player_YouTubeThumbnailLoader_OnThumbnailLoadedListener_ == IntPtr.Zero)
				id_setOnThumbnailLoadedListener_Lcom_google_android_youtube_player_YouTubeThumbnailLoader_OnThumbnailLoadedListener_ = JNIEnv.GetMethodID (class_ref, "setOnThumbnailLoadedListener", "(Lcom/google/android/youtube/player/YouTubeThumbnailLoader$OnThumbnailLoadedListener;)V");
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_setOnThumbnailLoadedListener_Lcom_google_android_youtube_player_YouTubeThumbnailLoader_OnThumbnailLoadedListener_, __args);
		}

		static Delegate cb_setPlaylist_Ljava_lang_String_;
#pragma warning disable 0169
		static Delegate GetSetPlaylist_Ljava_lang_String_Handler ()
		{
			if (cb_setPlaylist_Ljava_lang_String_ == null)
				cb_setPlaylist_Ljava_lang_String_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr>) n_SetPlaylist_Ljava_lang_String_);
			return cb_setPlaylist_Ljava_lang_String_;
		}

		static void n_SetPlaylist_Ljava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Google.YouTube.Player.IYouTubeThumbnailLoader __this = global::Java.Lang.Object.GetObject<global::Google.YouTube.Player.IYouTubeThumbnailLoader> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.SetPlaylist (p0);
		}
#pragma warning restore 0169

		IntPtr id_setPlaylist_Ljava_lang_String_;
		public unsafe void SetPlaylist (string p0)
		{
			if (id_setPlaylist_Ljava_lang_String_ == IntPtr.Zero)
				id_setPlaylist_Ljava_lang_String_ = JNIEnv.GetMethodID (class_ref, "setPlaylist", "(Ljava/lang/String;)V");
			IntPtr native_p0 = JNIEnv.NewString (p0);
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue (native_p0);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_setPlaylist_Ljava_lang_String_, __args);
			JNIEnv.DeleteLocalRef (native_p0);
		}

		static Delegate cb_setPlaylist_Ljava_lang_String_I;
#pragma warning disable 0169
		static Delegate GetSetPlaylist_Ljava_lang_String_IHandler ()
		{
			if (cb_setPlaylist_Ljava_lang_String_I == null)
				cb_setPlaylist_Ljava_lang_String_I = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr, int>) n_SetPlaylist_Ljava_lang_String_I);
			return cb_setPlaylist_Ljava_lang_String_I;
		}

		static void n_SetPlaylist_Ljava_lang_String_I (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, int p1)
		{
			global::Google.YouTube.Player.IYouTubeThumbnailLoader __this = global::Java.Lang.Object.GetObject<global::Google.YouTube.Player.IYouTubeThumbnailLoader> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.SetPlaylist (p0, p1);
		}
#pragma warning restore 0169

		IntPtr id_setPlaylist_Ljava_lang_String_I;
		public unsafe void SetPlaylist (string p0, int p1)
		{
			if (id_setPlaylist_Ljava_lang_String_I == IntPtr.Zero)
				id_setPlaylist_Ljava_lang_String_I = JNIEnv.GetMethodID (class_ref, "setPlaylist", "(Ljava/lang/String;I)V");
			IntPtr native_p0 = JNIEnv.NewString (p0);
			JValue* __args = stackalloc JValue [2];
			__args [0] = new JValue (native_p0);
			__args [1] = new JValue (p1);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_setPlaylist_Ljava_lang_String_I, __args);
			JNIEnv.DeleteLocalRef (native_p0);
		}

		static Delegate cb_setVideo_Ljava_lang_String_;
#pragma warning disable 0169
		static Delegate GetSetVideo_Ljava_lang_String_Handler ()
		{
			if (cb_setVideo_Ljava_lang_String_ == null)
				cb_setVideo_Ljava_lang_String_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr>) n_SetVideo_Ljava_lang_String_);
			return cb_setVideo_Ljava_lang_String_;
		}

		static void n_SetVideo_Ljava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Google.YouTube.Player.IYouTubeThumbnailLoader __this = global::Java.Lang.Object.GetObject<global::Google.YouTube.Player.IYouTubeThumbnailLoader> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.SetVideo (p0);
		}
#pragma warning restore 0169

		IntPtr id_setVideo_Ljava_lang_String_;
		public unsafe void SetVideo (string p0)
		{
			if (id_setVideo_Ljava_lang_String_ == IntPtr.Zero)
				id_setVideo_Ljava_lang_String_ = JNIEnv.GetMethodID (class_ref, "setVideo", "(Ljava/lang/String;)V");
			IntPtr native_p0 = JNIEnv.NewString (p0);
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue (native_p0);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_setVideo_Ljava_lang_String_, __args);
			JNIEnv.DeleteLocalRef (native_p0);
		}

	}

}

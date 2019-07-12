package md53d9588e8e69f0fd4fe1c851b9a1cfc47;


public class AndroidAutoSuggestBox
	extends android.widget.AutoCompleteTextView
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onFocusChanged:(ZILandroid/graphics/Rect;)V:GetOnFocusChanged_ZILandroid_graphics_Rect_Handler\n" +
			"n_onTextChanged:(Ljava/lang/CharSequence;III)V:GetOnTextChanged_Ljava_lang_CharSequence_IIIHandler\n" +
			"n_onEditorAction:(I)V:GetOnEditorAction_IHandler\n" +
			"n_replaceText:(Ljava/lang/CharSequence;)V:GetReplaceText_Ljava_lang_CharSequence_Handler\n" +
			"";
		mono.android.Runtime.register ("dotMorten.Xamarin.Forms.Platform.Android.AndroidAutoSuggestBox, dotMorten.Xamarin.Forms.AutoSuggestBox", AndroidAutoSuggestBox.class, __md_methods);
	}


	public AndroidAutoSuggestBox (android.content.Context p0)
	{
		super (p0);
		if (getClass () == AndroidAutoSuggestBox.class)
			mono.android.TypeManager.Activate ("dotMorten.Xamarin.Forms.Platform.Android.AndroidAutoSuggestBox, dotMorten.Xamarin.Forms.AutoSuggestBox", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public AndroidAutoSuggestBox (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == AndroidAutoSuggestBox.class)
			mono.android.TypeManager.Activate ("dotMorten.Xamarin.Forms.Platform.Android.AndroidAutoSuggestBox, dotMorten.Xamarin.Forms.AutoSuggestBox", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public AndroidAutoSuggestBox (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == AndroidAutoSuggestBox.class)
			mono.android.TypeManager.Activate ("dotMorten.Xamarin.Forms.Platform.Android.AndroidAutoSuggestBox, dotMorten.Xamarin.Forms.AutoSuggestBox", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public AndroidAutoSuggestBox (android.content.Context p0, android.util.AttributeSet p1, int p2, int p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == AndroidAutoSuggestBox.class)
			mono.android.TypeManager.Activate ("dotMorten.Xamarin.Forms.Platform.Android.AndroidAutoSuggestBox, dotMorten.Xamarin.Forms.AutoSuggestBox", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2, p3 });
	}


	public AndroidAutoSuggestBox (android.content.Context p0, android.util.AttributeSet p1, int p2, int p3, android.content.res.Resources.Theme p4)
	{
		super (p0, p1, p2, p3, p4);
		if (getClass () == AndroidAutoSuggestBox.class)
			mono.android.TypeManager.Activate ("dotMorten.Xamarin.Forms.Platform.Android.AndroidAutoSuggestBox, dotMorten.Xamarin.Forms.AutoSuggestBox", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib:System.Int32, mscorlib:Android.Content.Res.Resources+Theme, Mono.Android", this, new java.lang.Object[] { p0, p1, p2, p3, p4 });
	}


	public void onFocusChanged (boolean p0, int p1, android.graphics.Rect p2)
	{
		n_onFocusChanged (p0, p1, p2);
	}

	private native void n_onFocusChanged (boolean p0, int p1, android.graphics.Rect p2);


	public void onTextChanged (java.lang.CharSequence p0, int p1, int p2, int p3)
	{
		n_onTextChanged (p0, p1, p2, p3);
	}

	private native void n_onTextChanged (java.lang.CharSequence p0, int p1, int p2, int p3);


	public void onEditorAction (int p0)
	{
		n_onEditorAction (p0);
	}

	private native void n_onEditorAction (int p0);


	public void replaceText (java.lang.CharSequence p0)
	{
		n_replaceText (p0);
	}

	private native void n_replaceText (java.lang.CharSequence p0);

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

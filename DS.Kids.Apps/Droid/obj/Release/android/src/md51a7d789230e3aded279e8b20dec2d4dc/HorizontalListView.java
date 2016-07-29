package md51a7d789230e3aded279e8b20dec2d4dc;


public class HorizontalListView
	extends android.widget.AdapterView
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_getSelectedView:()Landroid/view/View;:GetGetSelectedViewHandler\n" +
			"n_getAdapter:()Landroid/widget/Adapter;:GetGetAdapterHandler\n" +
			"n_setAdapter:(Landroid/widget/Adapter;)V:GetSetAdapter_Landroid_widget_Adapter_Handler\n" +
			"n_onLayout:(ZIIII)V:GetOnLayout_ZIIIIHandler\n" +
			"n_setSelection:(I)V:GetSetSelection_IHandler\n" +
			"n_dispatchTouchEvent:(Landroid/view/MotionEvent;)Z:GetDispatchTouchEvent_Landroid_view_MotionEvent_Handler\n" +
			"";
		mono.android.Runtime.register ("BRFX.Core.Droid.Controls.HorizontalListView, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", HorizontalListView.class, __md_methods);
	}


	public HorizontalListView (android.content.Context p0, android.util.AttributeSet p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == HorizontalListView.class)
			mono.android.TypeManager.Activate ("BRFX.Core.Droid.Controls.HorizontalListView, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public HorizontalListView (android.content.Context p0, android.util.AttributeSet p1, int p2) throws java.lang.Throwable
	{
		super (p0, p1, p2);
		if (getClass () == HorizontalListView.class)
			mono.android.TypeManager.Activate ("BRFX.Core.Droid.Controls.HorizontalListView, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public android.view.View getSelectedView ()
	{
		return n_getSelectedView ();
	}

	private native android.view.View n_getSelectedView ();


	public android.widget.Adapter getAdapter ()
	{
		return n_getAdapter ();
	}

	private native android.widget.Adapter n_getAdapter ();


	public void setAdapter (android.widget.Adapter p0)
	{
		n_setAdapter (p0);
	}

	private native void n_setAdapter (android.widget.Adapter p0);


	public void onLayout (boolean p0, int p1, int p2, int p3, int p4)
	{
		n_onLayout (p0, p1, p2, p3, p4);
	}

	private native void n_onLayout (boolean p0, int p1, int p2, int p3, int p4);


	public void setSelection (int p0)
	{
		n_setSelection (p0);
	}

	private native void n_setSelection (int p0);


	public boolean dispatchTouchEvent (android.view.MotionEvent p0)
	{
		return n_dispatchTouchEvent (p0);
	}

	private native boolean n_dispatchTouchEvent (android.view.MotionEvent p0);

	java.util.ArrayList refList;
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

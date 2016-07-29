package cirrious.mvvmcross.droid.recyclerview;


public class MvxRecyclerView
	extends android.support.v7.widget.RecyclerView
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_setLayoutManager:(Landroid/support/v7/widget/RecyclerView$LayoutManager;)V:GetSetLayoutManager_Landroid_support_v7_widget_RecyclerView_LayoutManager_Handler\n" +
			"n_onScrolled:(II)V:GetOnScrolled_IIHandler\n" +
			"n_onScrollChanged:(IIII)V:GetOnScrollChanged_IIIIHandler\n" +
			"";
		mono.android.Runtime.register ("Cirrious.MvvmCross.Droid.RecyclerView.MvxRecyclerView, DS.Kids.Apps.Droid, Version=2.0.5.0, Culture=neutral, PublicKeyToken=null", MvxRecyclerView.class, __md_methods);
	}


	public MvxRecyclerView (android.content.Context p0, android.util.AttributeSet p1, int p2) throws java.lang.Throwable
	{
		super (p0, p1, p2);
		if (getClass () == MvxRecyclerView.class)
			mono.android.TypeManager.Activate ("Cirrious.MvvmCross.Droid.RecyclerView.MvxRecyclerView, DS.Kids.Apps.Droid, Version=2.0.5.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public MvxRecyclerView (android.content.Context p0, android.util.AttributeSet p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == MvxRecyclerView.class)
			mono.android.TypeManager.Activate ("Cirrious.MvvmCross.Droid.RecyclerView.MvxRecyclerView, DS.Kids.Apps.Droid, Version=2.0.5.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public MvxRecyclerView (android.content.Context p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == MvxRecyclerView.class)
			mono.android.TypeManager.Activate ("Cirrious.MvvmCross.Droid.RecyclerView.MvxRecyclerView, DS.Kids.Apps.Droid, Version=2.0.5.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void setLayoutManager (android.support.v7.widget.RecyclerView.LayoutManager p0)
	{
		n_setLayoutManager (p0);
	}

	private native void n_setLayoutManager (android.support.v7.widget.RecyclerView.LayoutManager p0);


	public void onScrolled (int p0, int p1)
	{
		n_onScrolled (p0, p1);
	}

	private native void n_onScrolled (int p0, int p1);


	public void onScrollChanged (int p0, int p1, int p2, int p3)
	{
		n_onScrollChanged (p0, p1, p2, p3);
	}

	private native void n_onScrollChanged (int p0, int p1, int p2, int p3);

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

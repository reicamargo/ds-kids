package md51a7d789230e3aded279e8b20dec2d4dc;


public class BindableViewPager
	extends android.support.v4.view.ViewPager
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("BRFX.Core.Droid.Controls.BindableViewPager, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BindableViewPager.class, __md_methods);
	}


	public BindableViewPager (android.content.Context p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == BindableViewPager.class)
			mono.android.TypeManager.Activate ("BRFX.Core.Droid.Controls.BindableViewPager, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public BindableViewPager (android.content.Context p0, android.util.AttributeSet p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == BindableViewPager.class)
			mono.android.TypeManager.Activate ("BRFX.Core.Droid.Controls.BindableViewPager, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}

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

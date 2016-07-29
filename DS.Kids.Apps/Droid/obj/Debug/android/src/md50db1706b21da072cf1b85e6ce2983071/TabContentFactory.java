package md50db1706b21da072cf1b85e6ce2983071;


public class TabContentFactory
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.widget.TabHost.TabContentFactory
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_createTabContent:(Ljava/lang/String;)Landroid/view/View;:GetCreateTabContent_Ljava_lang_String_Handler:Android.Widget.TabHost/ITabContentFactoryInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("BRFX.Core.Droid.Views.TabContentFactory, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", TabContentFactory.class, __md_methods);
	}


	public TabContentFactory () throws java.lang.Throwable
	{
		super ();
		if (getClass () == TabContentFactory.class)
			mono.android.TypeManager.Activate ("BRFX.Core.Droid.Views.TabContentFactory, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public TabContentFactory (android.view.View p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == TabContentFactory.class)
			mono.android.TypeManager.Activate ("BRFX.Core.Droid.Views.TabContentFactory, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Views.View, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public android.view.View createTabContent (java.lang.String p0)
	{
		return n_createTabContent (p0);
	}

	private native android.view.View n_createTabContent (java.lang.String p0);

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

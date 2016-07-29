package md5949073af779a84ae1cc8a401676c7b14;


public class BaseHomeChildView
	extends md50db1706b21da072cf1b85e6ce2983071.BaseView
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("DS.Kids.Apps.Droid.Views.BaseHomeChildView, DS.Kids.Apps.Droid, Version=2.0.5.0, Culture=neutral, PublicKeyToken=null", BaseHomeChildView.class, __md_methods);
	}


	public BaseHomeChildView () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BaseHomeChildView.class)
			mono.android.TypeManager.Activate ("DS.Kids.Apps.Droid.Views.BaseHomeChildView, DS.Kids.Apps.Droid, Version=2.0.5.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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

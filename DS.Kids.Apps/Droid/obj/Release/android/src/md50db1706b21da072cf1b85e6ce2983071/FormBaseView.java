package md50db1706b21da072cf1b85e6ce2983071;


public abstract class FormBaseView
	extends md50db1706b21da072cf1b85e6ce2983071.BaseView
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("BRFX.Core.Droid.Views.FormBaseView, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", FormBaseView.class, __md_methods);
	}


	public FormBaseView () throws java.lang.Throwable
	{
		super ();
		if (getClass () == FormBaseView.class)
			mono.android.TypeManager.Activate ("BRFX.Core.Droid.Views.FormBaseView, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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

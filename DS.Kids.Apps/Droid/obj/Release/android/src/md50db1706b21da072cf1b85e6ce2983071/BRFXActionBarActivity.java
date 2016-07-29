package md50db1706b21da072cf1b85e6ce2983071;


public class BRFXActionBarActivity
	extends md50db1706b21da072cf1b85e6ce2983071.BRFXActionBarEventSourceActivity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_setContentView:(I)V:GetSetContentView_IHandler\n" +
			"";
		mono.android.Runtime.register ("BRFX.Core.Droid.Views.BRFXActionBarActivity, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BRFXActionBarActivity.class, __md_methods);
	}


	public BRFXActionBarActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BRFXActionBarActivity.class)
			mono.android.TypeManager.Activate ("BRFX.Core.Droid.Views.BRFXActionBarActivity, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void setContentView (int p0)
	{
		n_setContentView (p0);
	}

	private native void n_setContentView (int p0);

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

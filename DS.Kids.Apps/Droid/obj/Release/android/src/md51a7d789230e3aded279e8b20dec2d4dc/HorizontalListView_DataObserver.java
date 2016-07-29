package md51a7d789230e3aded279e8b20dec2d4dc;


public class HorizontalListView_DataObserver
	extends android.database.DataSetObserver
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onChanged:()V:GetOnChangedHandler\n" +
			"n_onInvalidated:()V:GetOnInvalidatedHandler\n" +
			"";
		mono.android.Runtime.register ("BRFX.Core.Droid.Controls.HorizontalListView+DataObserver, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", HorizontalListView_DataObserver.class, __md_methods);
	}


	public HorizontalListView_DataObserver () throws java.lang.Throwable
	{
		super ();
		if (getClass () == HorizontalListView_DataObserver.class)
			mono.android.TypeManager.Activate ("BRFX.Core.Droid.Controls.HorizontalListView+DataObserver, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public HorizontalListView_DataObserver (md51a7d789230e3aded279e8b20dec2d4dc.HorizontalListView p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == HorizontalListView_DataObserver.class)
			mono.android.TypeManager.Activate ("BRFX.Core.Droid.Controls.HorizontalListView+DataObserver, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "BRFX.Core.Droid.Controls.HorizontalListView, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


	public void onChanged ()
	{
		n_onChanged ();
	}

	private native void n_onChanged ();


	public void onInvalidated ()
	{
		n_onInvalidated ();
	}

	private native void n_onInvalidated ();

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

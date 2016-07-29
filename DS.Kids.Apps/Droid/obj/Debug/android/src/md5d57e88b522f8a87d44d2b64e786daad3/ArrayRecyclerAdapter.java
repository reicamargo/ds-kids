package md5d57e88b522f8a87d44d2b64e786daad3;


public abstract class ArrayRecyclerAdapter
	extends android.support.v7.widget.RecyclerView.Adapter
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("DS.Kids.Apps.Droid.Controls.ArrayRecyclerAdapter, DS.Kids.Apps.Droid, Version=2.0.5.0, Culture=neutral, PublicKeyToken=null", ArrayRecyclerAdapter.class, __md_methods);
	}


	public ArrayRecyclerAdapter () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ArrayRecyclerAdapter.class)
			mono.android.TypeManager.Activate ("DS.Kids.Apps.Droid.Controls.ArrayRecyclerAdapter, DS.Kids.Apps.Droid, Version=2.0.5.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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

package md51a7d789230e3aded279e8b20dec2d4dc;


public class FirstLastAdapter
	extends md53471cbf751f08dad2f5f63288aefa6f2.MvxAdapter
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_getViewTypeCount:()I:GetGetViewTypeCountHandler\n" +
			"n_getItemViewType:(I)I:GetGetItemViewType_IHandler\n" +
			"";
		mono.android.Runtime.register ("BRFX.Core.Droid.Controls.FirstLastAdapter, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", FirstLastAdapter.class, __md_methods);
	}


	public FirstLastAdapter () throws java.lang.Throwable
	{
		super ();
		if (getClass () == FirstLastAdapter.class)
			mono.android.TypeManager.Activate ("BRFX.Core.Droid.Controls.FirstLastAdapter, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public FirstLastAdapter (android.content.Context p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == FirstLastAdapter.class)
			mono.android.TypeManager.Activate ("BRFX.Core.Droid.Controls.FirstLastAdapter, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public int getViewTypeCount ()
	{
		return n_getViewTypeCount ();
	}

	private native int n_getViewTypeCount ();


	public int getItemViewType (int p0)
	{
		return n_getItemViewType (p0);
	}

	private native int n_getItemViewType (int p0);

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

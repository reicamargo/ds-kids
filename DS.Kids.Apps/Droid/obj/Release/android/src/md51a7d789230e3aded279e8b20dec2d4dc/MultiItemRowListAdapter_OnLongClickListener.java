package md51a7d789230e3aded279e8b20dec2d4dc;


public class MultiItemRowListAdapter_OnLongClickListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.view.View.OnLongClickListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onLongClick:(Landroid/view/View;)Z:GetOnLongClick_Landroid_view_View_Handler:Android.Views.View/IOnLongClickListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("BRFX.Core.Droid.Controls.MultiItemRowListAdapter+OnLongClickListener, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MultiItemRowListAdapter_OnLongClickListener.class, __md_methods);
	}


	public MultiItemRowListAdapter_OnLongClickListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MultiItemRowListAdapter_OnLongClickListener.class)
			mono.android.TypeManager.Activate ("BRFX.Core.Droid.Controls.MultiItemRowListAdapter+OnLongClickListener, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public boolean onLongClick (android.view.View p0)
	{
		return n_onLongClick (p0);
	}

	private native boolean n_onLongClick (android.view.View p0);

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

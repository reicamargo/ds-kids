package md51a7d789230e3aded279e8b20dec2d4dc;


public class MultiItemRowListAdapter_OnClickListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.view.View.OnClickListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onClick:(Landroid/view/View;)V:GetOnClick_Landroid_view_View_Handler:Android.Views.View/IOnClickListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("BRFX.Core.Droid.Controls.MultiItemRowListAdapter+OnClickListener, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MultiItemRowListAdapter_OnClickListener.class, __md_methods);
	}


	public MultiItemRowListAdapter_OnClickListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MultiItemRowListAdapter_OnClickListener.class)
			mono.android.TypeManager.Activate ("BRFX.Core.Droid.Controls.MultiItemRowListAdapter+OnClickListener, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onClick (android.view.View p0)
	{
		n_onClick (p0);
	}

	private native void n_onClick (android.view.View p0);

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

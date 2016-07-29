package md50db1706b21da072cf1b85e6ce2983071;


public class BRFXTabHostBaseView
	extends md50db1706b21da072cf1b85e6ce2983071.BaseView
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreateView:(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;:GetOnCreateView_Landroid_view_LayoutInflater_Landroid_view_ViewGroup_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("BRFX.Core.Droid.Views.BRFXTabHostBaseView, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BRFXTabHostBaseView.class, __md_methods);
	}


	public BRFXTabHostBaseView () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BRFXTabHostBaseView.class)
			mono.android.TypeManager.Activate ("BRFX.Core.Droid.Views.BRFXTabHostBaseView, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public android.view.View onCreateView (android.view.LayoutInflater p0, android.view.ViewGroup p1, android.os.Bundle p2)
	{
		return n_onCreateView (p0, p1, p2);
	}

	private native android.view.View n_onCreateView (android.view.LayoutInflater p0, android.view.ViewGroup p1, android.os.Bundle p2);

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

package md55dde1a1a54831bb29b0aaea2e147a8f2;


public class OnBootReceiver
	extends android.content.BroadcastReceiver
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onReceive:(Landroid/content/Context;Landroid/content/Intent;)V:GetOnReceive_Landroid_content_Context_Landroid_content_Intent_Handler\n" +
			"";
		mono.android.Runtime.register ("DS.Kids.Apps.Droid.Plugins.OnBootReceiver, DS.Kids.Apps.Droid, Version=2.0.5.0, Culture=neutral, PublicKeyToken=null", OnBootReceiver.class, __md_methods);
	}


	public OnBootReceiver () throws java.lang.Throwable
	{
		super ();
		if (getClass () == OnBootReceiver.class)
			mono.android.TypeManager.Activate ("DS.Kids.Apps.Droid.Plugins.OnBootReceiver, DS.Kids.Apps.Droid, Version=2.0.5.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onReceive (android.content.Context p0, android.content.Intent p1)
	{
		n_onReceive (p0, p1);
	}

	private native void n_onReceive (android.content.Context p0, android.content.Intent p1);

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

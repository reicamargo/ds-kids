package mono.com.mopub.mobileads;


public class MoPubView_OnAdClosedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.mopub.mobileads.MoPubView.OnAdClosedListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_OnAdClosed:(Lcom/mopub/mobileads/MoPubView;)V:GetOnAdClosed_Lcom_mopub_mobileads_MoPubView_Handler:MoPub.MobileAds.MoPubView/IOnAdClosedListenerInvoker, MoPubAndroid\n" +
			"";
		mono.android.Runtime.register ("MoPub.MobileAds.MoPubView+IOnAdClosedListenerImplementor, MoPubAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MoPubView_OnAdClosedListenerImplementor.class, __md_methods);
	}


	public MoPubView_OnAdClosedListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MoPubView_OnAdClosedListenerImplementor.class)
			mono.android.TypeManager.Activate ("MoPub.MobileAds.MoPubView+IOnAdClosedListenerImplementor, MoPubAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void OnAdClosed (com.mopub.mobileads.MoPubView p0)
	{
		n_OnAdClosed (p0);
	}

	private native void n_OnAdClosed (com.mopub.mobileads.MoPubView p0);

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

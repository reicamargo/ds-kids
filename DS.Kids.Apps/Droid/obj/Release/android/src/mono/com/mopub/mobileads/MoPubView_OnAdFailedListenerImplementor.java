package mono.com.mopub.mobileads;


public class MoPubView_OnAdFailedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.mopub.mobileads.MoPubView.OnAdFailedListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_OnAdFailed:(Lcom/mopub/mobileads/MoPubView;)V:GetOnAdFailed_Lcom_mopub_mobileads_MoPubView_Handler:MoPub.MobileAds.MoPubView/IOnAdFailedListenerInvoker, MoPubAndroid\n" +
			"";
		mono.android.Runtime.register ("MoPub.MobileAds.MoPubView+IOnAdFailedListenerImplementor, MoPubAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MoPubView_OnAdFailedListenerImplementor.class, __md_methods);
	}


	public MoPubView_OnAdFailedListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MoPubView_OnAdFailedListenerImplementor.class)
			mono.android.TypeManager.Activate ("MoPub.MobileAds.MoPubView+IOnAdFailedListenerImplementor, MoPubAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void OnAdFailed (com.mopub.mobileads.MoPubView p0)
	{
		n_OnAdFailed (p0);
	}

	private native void n_OnAdFailed (com.mopub.mobileads.MoPubView p0);

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

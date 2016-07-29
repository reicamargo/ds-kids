package mono.com.mopub.mobileads;


public class MoPubView_OnAdClickedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.mopub.mobileads.MoPubView.OnAdClickedListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_OnAdClicked:(Lcom/mopub/mobileads/MoPubView;)V:GetOnAdClicked_Lcom_mopub_mobileads_MoPubView_Handler:MoPub.MobileAds.MoPubView/IOnAdClickedListenerInvoker, MoPubAndroid\n" +
			"";
		mono.android.Runtime.register ("MoPub.MobileAds.MoPubView+IOnAdClickedListenerImplementor, MoPubAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MoPubView_OnAdClickedListenerImplementor.class, __md_methods);
	}


	public MoPubView_OnAdClickedListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MoPubView_OnAdClickedListenerImplementor.class)
			mono.android.TypeManager.Activate ("MoPub.MobileAds.MoPubView+IOnAdClickedListenerImplementor, MoPubAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void OnAdClicked (com.mopub.mobileads.MoPubView p0)
	{
		n_OnAdClicked (p0);
	}

	private native void n_OnAdClicked (com.mopub.mobileads.MoPubView p0);

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

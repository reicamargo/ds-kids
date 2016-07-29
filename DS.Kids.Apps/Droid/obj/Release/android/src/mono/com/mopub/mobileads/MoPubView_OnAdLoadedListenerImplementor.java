package mono.com.mopub.mobileads;


public class MoPubView_OnAdLoadedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.mopub.mobileads.MoPubView.OnAdLoadedListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_OnAdLoaded:(Lcom/mopub/mobileads/MoPubView;)V:GetOnAdLoaded_Lcom_mopub_mobileads_MoPubView_Handler:MoPub.MobileAds.MoPubView/IOnAdLoadedListenerInvoker, MoPubAndroid\n" +
			"";
		mono.android.Runtime.register ("MoPub.MobileAds.MoPubView+IOnAdLoadedListenerImplementor, MoPubAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MoPubView_OnAdLoadedListenerImplementor.class, __md_methods);
	}


	public MoPubView_OnAdLoadedListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MoPubView_OnAdLoadedListenerImplementor.class)
			mono.android.TypeManager.Activate ("MoPub.MobileAds.MoPubView+IOnAdLoadedListenerImplementor, MoPubAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void OnAdLoaded (com.mopub.mobileads.MoPubView p0)
	{
		n_OnAdLoaded (p0);
	}

	private native void n_OnAdLoaded (com.mopub.mobileads.MoPubView p0);

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

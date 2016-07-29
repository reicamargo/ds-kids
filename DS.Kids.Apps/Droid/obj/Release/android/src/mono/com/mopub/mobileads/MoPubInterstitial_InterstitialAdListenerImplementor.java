package mono.com.mopub.mobileads;


public class MoPubInterstitial_InterstitialAdListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.mopub.mobileads.MoPubInterstitial.InterstitialAdListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onInterstitialClicked:(Lcom/mopub/mobileads/MoPubInterstitial;)V:GetOnInterstitialClicked_Lcom_mopub_mobileads_MoPubInterstitial_Handler:MoPub.MobileAds.MoPubInterstitial/IInterstitialAdListenerInvoker, MoPubAndroid\n" +
			"n_onInterstitialDismissed:(Lcom/mopub/mobileads/MoPubInterstitial;)V:GetOnInterstitialDismissed_Lcom_mopub_mobileads_MoPubInterstitial_Handler:MoPub.MobileAds.MoPubInterstitial/IInterstitialAdListenerInvoker, MoPubAndroid\n" +
			"n_onInterstitialFailed:(Lcom/mopub/mobileads/MoPubInterstitial;Lcom/mopub/mobileads/MoPubErrorCode;)V:GetOnInterstitialFailed_Lcom_mopub_mobileads_MoPubInterstitial_Lcom_mopub_mobileads_MoPubErrorCode_Handler:MoPub.MobileAds.MoPubInterstitial/IInterstitialAdListenerInvoker, MoPubAndroid\n" +
			"n_onInterstitialLoaded:(Lcom/mopub/mobileads/MoPubInterstitial;)V:GetOnInterstitialLoaded_Lcom_mopub_mobileads_MoPubInterstitial_Handler:MoPub.MobileAds.MoPubInterstitial/IInterstitialAdListenerInvoker, MoPubAndroid\n" +
			"n_onInterstitialShown:(Lcom/mopub/mobileads/MoPubInterstitial;)V:GetOnInterstitialShown_Lcom_mopub_mobileads_MoPubInterstitial_Handler:MoPub.MobileAds.MoPubInterstitial/IInterstitialAdListenerInvoker, MoPubAndroid\n" +
			"";
		mono.android.Runtime.register ("MoPub.MobileAds.MoPubInterstitial+IInterstitialAdListenerImplementor, MoPubAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MoPubInterstitial_InterstitialAdListenerImplementor.class, __md_methods);
	}


	public MoPubInterstitial_InterstitialAdListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MoPubInterstitial_InterstitialAdListenerImplementor.class)
			mono.android.TypeManager.Activate ("MoPub.MobileAds.MoPubInterstitial+IInterstitialAdListenerImplementor, MoPubAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onInterstitialClicked (com.mopub.mobileads.MoPubInterstitial p0)
	{
		n_onInterstitialClicked (p0);
	}

	private native void n_onInterstitialClicked (com.mopub.mobileads.MoPubInterstitial p0);


	public void onInterstitialDismissed (com.mopub.mobileads.MoPubInterstitial p0)
	{
		n_onInterstitialDismissed (p0);
	}

	private native void n_onInterstitialDismissed (com.mopub.mobileads.MoPubInterstitial p0);


	public void onInterstitialFailed (com.mopub.mobileads.MoPubInterstitial p0, com.mopub.mobileads.MoPubErrorCode p1)
	{
		n_onInterstitialFailed (p0, p1);
	}

	private native void n_onInterstitialFailed (com.mopub.mobileads.MoPubInterstitial p0, com.mopub.mobileads.MoPubErrorCode p1);


	public void onInterstitialLoaded (com.mopub.mobileads.MoPubInterstitial p0)
	{
		n_onInterstitialLoaded (p0);
	}

	private native void n_onInterstitialLoaded (com.mopub.mobileads.MoPubInterstitial p0);


	public void onInterstitialShown (com.mopub.mobileads.MoPubInterstitial p0)
	{
		n_onInterstitialShown (p0);
	}

	private native void n_onInterstitialShown (com.mopub.mobileads.MoPubInterstitial p0);

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

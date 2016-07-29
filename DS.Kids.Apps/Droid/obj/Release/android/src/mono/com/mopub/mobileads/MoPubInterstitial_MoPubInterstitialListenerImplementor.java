package mono.com.mopub.mobileads;


public class MoPubInterstitial_MoPubInterstitialListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.mopub.mobileads.MoPubInterstitial.MoPubInterstitialListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_OnInterstitialFailed:()V:GetOnInterstitialFailedHandler:MoPub.MobileAds.MoPubInterstitial/IMoPubInterstitialListenerInvoker, MoPubAndroid\n" +
			"n_OnInterstitialLoaded:()V:GetOnInterstitialLoadedHandler:MoPub.MobileAds.MoPubInterstitial/IMoPubInterstitialListenerInvoker, MoPubAndroid\n" +
			"";
		mono.android.Runtime.register ("MoPub.MobileAds.MoPubInterstitial+IMoPubInterstitialListenerImplementor, MoPubAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MoPubInterstitial_MoPubInterstitialListenerImplementor.class, __md_methods);
	}


	public MoPubInterstitial_MoPubInterstitialListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MoPubInterstitial_MoPubInterstitialListenerImplementor.class)
			mono.android.TypeManager.Activate ("MoPub.MobileAds.MoPubInterstitial+IMoPubInterstitialListenerImplementor, MoPubAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void OnInterstitialFailed ()
	{
		n_OnInterstitialFailed ();
	}

	private native void n_OnInterstitialFailed ();


	public void OnInterstitialLoaded ()
	{
		n_OnInterstitialLoaded ();
	}

	private native void n_OnInterstitialLoaded ();

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

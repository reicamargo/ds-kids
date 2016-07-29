package mono.com.mopub.mobileads;


public class MoPubView_OnAdPresentedOverlayListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.mopub.mobileads.MoPubView.OnAdPresentedOverlayListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_OnAdPresentedOverlay:(Lcom/mopub/mobileads/MoPubView;)V:GetOnAdPresentedOverlay_Lcom_mopub_mobileads_MoPubView_Handler:MoPub.MobileAds.MoPubView/IOnAdPresentedOverlayListenerInvoker, MoPubAndroid\n" +
			"";
		mono.android.Runtime.register ("MoPub.MobileAds.MoPubView+IOnAdPresentedOverlayListenerImplementor, MoPubAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MoPubView_OnAdPresentedOverlayListenerImplementor.class, __md_methods);
	}


	public MoPubView_OnAdPresentedOverlayListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MoPubView_OnAdPresentedOverlayListenerImplementor.class)
			mono.android.TypeManager.Activate ("MoPub.MobileAds.MoPubView+IOnAdPresentedOverlayListenerImplementor, MoPubAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void OnAdPresentedOverlay (com.mopub.mobileads.MoPubView p0)
	{
		n_OnAdPresentedOverlay (p0);
	}

	private native void n_OnAdPresentedOverlay (com.mopub.mobileads.MoPubView p0);

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

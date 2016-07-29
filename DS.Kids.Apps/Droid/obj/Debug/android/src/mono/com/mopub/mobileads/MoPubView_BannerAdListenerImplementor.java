package mono.com.mopub.mobileads;


public class MoPubView_BannerAdListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.mopub.mobileads.MoPubView.BannerAdListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onBannerClicked:(Lcom/mopub/mobileads/MoPubView;)V:GetOnBannerClicked_Lcom_mopub_mobileads_MoPubView_Handler:MoPub.MobileAds.MoPubView/IBannerAdListenerInvoker, MoPubAndroid\n" +
			"n_onBannerCollapsed:(Lcom/mopub/mobileads/MoPubView;)V:GetOnBannerCollapsed_Lcom_mopub_mobileads_MoPubView_Handler:MoPub.MobileAds.MoPubView/IBannerAdListenerInvoker, MoPubAndroid\n" +
			"n_onBannerExpanded:(Lcom/mopub/mobileads/MoPubView;)V:GetOnBannerExpanded_Lcom_mopub_mobileads_MoPubView_Handler:MoPub.MobileAds.MoPubView/IBannerAdListenerInvoker, MoPubAndroid\n" +
			"n_onBannerFailed:(Lcom/mopub/mobileads/MoPubView;Lcom/mopub/mobileads/MoPubErrorCode;)V:GetOnBannerFailed_Lcom_mopub_mobileads_MoPubView_Lcom_mopub_mobileads_MoPubErrorCode_Handler:MoPub.MobileAds.MoPubView/IBannerAdListenerInvoker, MoPubAndroid\n" +
			"n_onBannerLoaded:(Lcom/mopub/mobileads/MoPubView;)V:GetOnBannerLoaded_Lcom_mopub_mobileads_MoPubView_Handler:MoPub.MobileAds.MoPubView/IBannerAdListenerInvoker, MoPubAndroid\n" +
			"";
		mono.android.Runtime.register ("MoPub.MobileAds.MoPubView+IBannerAdListenerImplementor, MoPubAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MoPubView_BannerAdListenerImplementor.class, __md_methods);
	}


	public MoPubView_BannerAdListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MoPubView_BannerAdListenerImplementor.class)
			mono.android.TypeManager.Activate ("MoPub.MobileAds.MoPubView+IBannerAdListenerImplementor, MoPubAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onBannerClicked (com.mopub.mobileads.MoPubView p0)
	{
		n_onBannerClicked (p0);
	}

	private native void n_onBannerClicked (com.mopub.mobileads.MoPubView p0);


	public void onBannerCollapsed (com.mopub.mobileads.MoPubView p0)
	{
		n_onBannerCollapsed (p0);
	}

	private native void n_onBannerCollapsed (com.mopub.mobileads.MoPubView p0);


	public void onBannerExpanded (com.mopub.mobileads.MoPubView p0)
	{
		n_onBannerExpanded (p0);
	}

	private native void n_onBannerExpanded (com.mopub.mobileads.MoPubView p0);


	public void onBannerFailed (com.mopub.mobileads.MoPubView p0, com.mopub.mobileads.MoPubErrorCode p1)
	{
		n_onBannerFailed (p0, p1);
	}

	private native void n_onBannerFailed (com.mopub.mobileads.MoPubView p0, com.mopub.mobileads.MoPubErrorCode p1);


	public void onBannerLoaded (com.mopub.mobileads.MoPubView p0)
	{
		n_onBannerLoaded (p0);
	}

	private native void n_onBannerLoaded (com.mopub.mobileads.MoPubView p0);

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

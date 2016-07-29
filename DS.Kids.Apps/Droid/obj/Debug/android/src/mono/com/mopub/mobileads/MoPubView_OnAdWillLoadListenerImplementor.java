package mono.com.mopub.mobileads;


public class MoPubView_OnAdWillLoadListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.mopub.mobileads.MoPubView.OnAdWillLoadListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_OnAdWillLoad:(Lcom/mopub/mobileads/MoPubView;Ljava/lang/String;)V:GetOnAdWillLoad_Lcom_mopub_mobileads_MoPubView_Ljava_lang_String_Handler:MoPub.MobileAds.MoPubView/IOnAdWillLoadListenerInvoker, MoPubAndroid\n" +
			"";
		mono.android.Runtime.register ("MoPub.MobileAds.MoPubView+IOnAdWillLoadListenerImplementor, MoPubAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MoPubView_OnAdWillLoadListenerImplementor.class, __md_methods);
	}


	public MoPubView_OnAdWillLoadListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MoPubView_OnAdWillLoadListenerImplementor.class)
			mono.android.TypeManager.Activate ("MoPub.MobileAds.MoPubView+IOnAdWillLoadListenerImplementor, MoPubAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void OnAdWillLoad (com.mopub.mobileads.MoPubView p0, java.lang.String p1)
	{
		n_OnAdWillLoad (p0, p1);
	}

	private native void n_OnAdWillLoad (com.mopub.mobileads.MoPubView p0, java.lang.String p1);

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

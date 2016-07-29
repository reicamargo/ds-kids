package md5971b7d827cf7b6568b54630233ce7f2d;


public class FacebookPlugin
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.facebook.FacebookCallback,
		com.facebook.GraphRequest.GraphJSONObjectCallback
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCancel:()V:GetOnCancelHandler:Xamarin.Facebook.IFacebookCallbackInvoker, Xamarin.Facebook\n" +
			"n_onError:(Lcom/facebook/FacebookException;)V:GetOnError_Lcom_facebook_FacebookException_Handler:Xamarin.Facebook.IFacebookCallbackInvoker, Xamarin.Facebook\n" +
			"n_onSuccess:(Ljava/lang/Object;)V:GetOnSuccess_Ljava_lang_Object_Handler:Xamarin.Facebook.IFacebookCallbackInvoker, Xamarin.Facebook\n" +
			"n_onCompleted:(Lorg/json/JSONObject;Lcom/facebook/GraphResponse;)V:GetOnCompleted_Lorg_json_JSONObject_Lcom_facebook_GraphResponse_Handler:Xamarin.Facebook.GraphRequest/IGraphJSONObjectCallbackInvoker, Xamarin.Facebook\n" +
			"";
		mono.android.Runtime.register ("BRFX.Core.Droid.Plugins.FacebookPlugin, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", FacebookPlugin.class, __md_methods);
	}


	public FacebookPlugin () throws java.lang.Throwable
	{
		super ();
		if (getClass () == FacebookPlugin.class)
			mono.android.TypeManager.Activate ("BRFX.Core.Droid.Plugins.FacebookPlugin, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCancel ()
	{
		n_onCancel ();
	}

	private native void n_onCancel ();


	public void onError (com.facebook.FacebookException p0)
	{
		n_onError (p0);
	}

	private native void n_onError (com.facebook.FacebookException p0);


	public void onSuccess (java.lang.Object p0)
	{
		n_onSuccess (p0);
	}

	private native void n_onSuccess (java.lang.Object p0);


	public void onCompleted (org.json.JSONObject p0, com.facebook.GraphResponse p1)
	{
		n_onCompleted (p0, p1);
	}

	private native void n_onCompleted (org.json.JSONObject p0, com.facebook.GraphResponse p1);

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

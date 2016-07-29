package md5c603d9e1ef639cdd8269b753e6ddf44f;


public class BounceEaseOut
	extends md5c603d9e1ef639cdd8269b753e6ddf44f.BaseEasingMethod
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("BRFX.Core.Droid.Helpers.Animations.BounceEaseOut, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BounceEaseOut.class, __md_methods);
	}


	public BounceEaseOut () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BounceEaseOut.class)
			mono.android.TypeManager.Activate ("BRFX.Core.Droid.Helpers.Animations.BounceEaseOut, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

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

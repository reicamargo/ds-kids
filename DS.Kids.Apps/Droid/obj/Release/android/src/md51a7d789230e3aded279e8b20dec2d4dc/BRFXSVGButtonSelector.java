package md51a7d789230e3aded279e8b20dec2d4dc;


public class BRFXSVGButtonSelector
	extends android.graphics.drawable.StateListDrawable
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("BRFX.Core.Droid.Controls.BRFXSVGButtonSelector, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BRFXSVGButtonSelector.class, __md_methods);
	}


	public BRFXSVGButtonSelector () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BRFXSVGButtonSelector.class)
			mono.android.TypeManager.Activate ("BRFX.Core.Droid.Controls.BRFXSVGButtonSelector, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public BRFXSVGButtonSelector (android.content.Context p0, java.lang.String p1, java.lang.String p2) throws java.lang.Throwable
	{
		super ();
		if (getClass () == BRFXSVGButtonSelector.class)
			mono.android.TypeManager.Activate ("BRFX.Core.Droid.Controls.BRFXSVGButtonSelector, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}

	public BRFXSVGButtonSelector (android.graphics.drawable.Drawable p0, android.graphics.drawable.Drawable p1) throws java.lang.Throwable
	{
		super ();
		if (getClass () == BRFXSVGButtonSelector.class)
			mono.android.TypeManager.Activate ("BRFX.Core.Droid.Controls.BRFXSVGButtonSelector, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Graphics.Drawables.Drawable, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Graphics.Drawables.Drawable, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
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

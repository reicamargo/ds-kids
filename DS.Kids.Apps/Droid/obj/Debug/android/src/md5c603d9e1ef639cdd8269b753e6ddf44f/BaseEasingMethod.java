package md5c603d9e1ef639cdd8269b753e6ddf44f;


public abstract class BaseEasingMethod
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.animation.TypeEvaluator
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_evaluate:(FLjava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;:GetEvaluate_FLjava_lang_Object_Ljava_lang_Object_Handler:Android.Animation.ITypeEvaluatorInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("BRFX.Core.Droid.Helpers.Animations.BaseEasingMethod, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BaseEasingMethod.class, __md_methods);
	}


	public BaseEasingMethod () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BaseEasingMethod.class)
			mono.android.TypeManager.Activate ("BRFX.Core.Droid.Helpers.Animations.BaseEasingMethod, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public java.lang.Object evaluate (float p0, java.lang.Object p1, java.lang.Object p2)
	{
		return n_evaluate (p0, p1, p2);
	}

	private native java.lang.Object n_evaluate (float p0, java.lang.Object p1, java.lang.Object p2);

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
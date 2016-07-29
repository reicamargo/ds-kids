package md5bde12959bffa8278429445232de2f112;


public class AnimatorSetHelpers_ColorAnimatorUpdateListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.animation.ValueAnimator.AnimatorUpdateListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onAnimationUpdate:(Landroid/animation/ValueAnimator;)V:GetOnAnimationUpdate_Landroid_animation_ValueAnimator_Handler:Android.Animation.ValueAnimator/IAnimatorUpdateListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("BRFX.Core.Droid.Helpers.AnimatorSetHelpers+ColorAnimatorUpdateListener, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", AnimatorSetHelpers_ColorAnimatorUpdateListener.class, __md_methods);
	}


	public AnimatorSetHelpers_ColorAnimatorUpdateListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == AnimatorSetHelpers_ColorAnimatorUpdateListener.class)
			mono.android.TypeManager.Activate ("BRFX.Core.Droid.Helpers.AnimatorSetHelpers+ColorAnimatorUpdateListener, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public AnimatorSetHelpers_ColorAnimatorUpdateListener (android.view.View p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == AnimatorSetHelpers_ColorAnimatorUpdateListener.class)
			mono.android.TypeManager.Activate ("BRFX.Core.Droid.Helpers.AnimatorSetHelpers+ColorAnimatorUpdateListener, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Views.View, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void onAnimationUpdate (android.animation.ValueAnimator p0)
	{
		n_onAnimationUpdate (p0);
	}

	private native void n_onAnimationUpdate (android.animation.ValueAnimator p0);

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

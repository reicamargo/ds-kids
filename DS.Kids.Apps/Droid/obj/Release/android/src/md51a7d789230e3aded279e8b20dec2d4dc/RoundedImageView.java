package md51a7d789230e3aded279e8b20dec2d4dc;


public class RoundedImageView
	extends cirrious.mvvmcross.binding.droid.views.MvxImageView
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_drawableStateChanged:()V:GetDrawableStateChangedHandler\n" +
			"n_getScaleType:()Landroid/widget/ImageView$ScaleType;:GetGetScaleTypeHandler\n" +
			"n_setScaleType:(Landroid/widget/ImageView$ScaleType;)V:GetSetScaleType_Landroid_widget_ImageView_ScaleType_Handler\n" +
			"n_setImageDrawable:(Landroid/graphics/drawable/Drawable;)V:GetSetImageDrawable_Landroid_graphics_drawable_Drawable_Handler\n" +
			"n_setImageBitmap:(Landroid/graphics/Bitmap;)V:GetSetImageBitmap_Landroid_graphics_Bitmap_Handler\n" +
			"n_setImageResource:(I)V:GetSetImageResource_IHandler\n" +
			"n_setImageURI:(Landroid/net/Uri;)V:GetSetImageURI_Landroid_net_Uri_Handler\n" +
			"n_setBackgroundDrawable:(Landroid/graphics/drawable/Drawable;)V:GetSetBackgroundDrawable_Landroid_graphics_drawable_Drawable_Handler\n" +
			"";
		mono.android.Runtime.register ("BRFX.Core.Droid.Controls.RoundedImageView, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", RoundedImageView.class, __md_methods);
	}


	public RoundedImageView (android.content.Context p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == RoundedImageView.class)
			mono.android.TypeManager.Activate ("BRFX.Core.Droid.Controls.RoundedImageView, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public RoundedImageView (android.content.Context p0, android.util.AttributeSet p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == RoundedImageView.class)
			mono.android.TypeManager.Activate ("BRFX.Core.Droid.Controls.RoundedImageView, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public RoundedImageView (android.content.Context p0, android.util.AttributeSet p1, int p2) throws java.lang.Throwable
	{
		super (p0, p1, p2);
		if (getClass () == RoundedImageView.class)
			mono.android.TypeManager.Activate ("BRFX.Core.Droid.Controls.RoundedImageView, BRFX.Core.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public void drawableStateChanged ()
	{
		n_drawableStateChanged ();
	}

	private native void n_drawableStateChanged ();


	public android.widget.ImageView.ScaleType getScaleType ()
	{
		return n_getScaleType ();
	}

	private native android.widget.ImageView.ScaleType n_getScaleType ();


	public void setScaleType (android.widget.ImageView.ScaleType p0)
	{
		n_setScaleType (p0);
	}

	private native void n_setScaleType (android.widget.ImageView.ScaleType p0);


	public void setImageDrawable (android.graphics.drawable.Drawable p0)
	{
		n_setImageDrawable (p0);
	}

	private native void n_setImageDrawable (android.graphics.drawable.Drawable p0);


	public void setImageBitmap (android.graphics.Bitmap p0)
	{
		n_setImageBitmap (p0);
	}

	private native void n_setImageBitmap (android.graphics.Bitmap p0);


	public void setImageResource (int p0)
	{
		n_setImageResource (p0);
	}

	private native void n_setImageResource (int p0);


	public void setImageURI (android.net.Uri p0)
	{
		n_setImageURI (p0);
	}

	private native void n_setImageURI (android.net.Uri p0);


	public void setBackgroundDrawable (android.graphics.drawable.Drawable p0)
	{
		n_setBackgroundDrawable (p0);
	}

	private native void n_setBackgroundDrawable (android.graphics.drawable.Drawable p0);

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

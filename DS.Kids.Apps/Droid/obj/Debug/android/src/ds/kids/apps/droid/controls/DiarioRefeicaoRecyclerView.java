package ds.kids.apps.droid.controls;


public class DiarioRefeicaoRecyclerView
	extends md5d57e88b522f8a87d44d2b64e786daad3.HeaderFooterRecyclerView_1
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("DS.Kids.Apps.Droid.Controls.DiarioRefeicaoRecyclerView, DS.Kids.Apps.Droid, Version=2.0.5.0, Culture=neutral, PublicKeyToken=null", DiarioRefeicaoRecyclerView.class, __md_methods);
	}


	public DiarioRefeicaoRecyclerView (android.content.Context p0, android.util.AttributeSet p1, int p2) throws java.lang.Throwable
	{
		super (p0, p1, p2);
		if (getClass () == DiarioRefeicaoRecyclerView.class)
			mono.android.TypeManager.Activate ("DS.Kids.Apps.Droid.Controls.DiarioRefeicaoRecyclerView, DS.Kids.Apps.Droid, Version=2.0.5.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public DiarioRefeicaoRecyclerView (android.content.Context p0, android.util.AttributeSet p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == DiarioRefeicaoRecyclerView.class)
			mono.android.TypeManager.Activate ("DS.Kids.Apps.Droid.Controls.DiarioRefeicaoRecyclerView, DS.Kids.Apps.Droid, Version=2.0.5.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public DiarioRefeicaoRecyclerView (android.content.Context p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == DiarioRefeicaoRecyclerView.class)
			mono.android.TypeManager.Activate ("DS.Kids.Apps.Droid.Controls.DiarioRefeicaoRecyclerView, DS.Kids.Apps.Droid, Version=2.0.5.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
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
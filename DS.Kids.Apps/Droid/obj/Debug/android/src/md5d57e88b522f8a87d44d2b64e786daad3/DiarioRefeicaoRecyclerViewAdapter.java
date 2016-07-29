package md5d57e88b522f8a87d44d2b64e786daad3;


public class DiarioRefeicaoRecyclerViewAdapter
	extends md5d57e88b522f8a87d44d2b64e786daad3.HeaderFooterRecyclerViewAdapter_1
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreateViewHolder:(Landroid/view/ViewGroup;I)Landroid/support/v7/widget/RecyclerView$ViewHolder;:GetOnCreateViewHolder_Landroid_view_ViewGroup_IHandler\n" +
			"";
		mono.android.Runtime.register ("DS.Kids.Apps.Droid.Controls.DiarioRefeicaoRecyclerViewAdapter, DS.Kids.Apps.Droid, Version=2.0.5.0, Culture=neutral, PublicKeyToken=null", DiarioRefeicaoRecyclerViewAdapter.class, __md_methods);
	}


	public DiarioRefeicaoRecyclerViewAdapter () throws java.lang.Throwable
	{
		super ();
		if (getClass () == DiarioRefeicaoRecyclerViewAdapter.class)
			mono.android.TypeManager.Activate ("DS.Kids.Apps.Droid.Controls.DiarioRefeicaoRecyclerViewAdapter, DS.Kids.Apps.Droid, Version=2.0.5.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public android.support.v7.widget.RecyclerView.ViewHolder onCreateViewHolder (android.view.ViewGroup p0, int p1)
	{
		return n_onCreateViewHolder (p0, p1);
	}

	private native android.support.v7.widget.RecyclerView.ViewHolder n_onCreateViewHolder (android.view.ViewGroup p0, int p1);

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

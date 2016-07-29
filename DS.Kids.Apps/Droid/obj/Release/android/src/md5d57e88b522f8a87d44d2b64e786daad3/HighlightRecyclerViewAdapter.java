package md5d57e88b522f8a87d44d2b64e786daad3;


public abstract class HighlightRecyclerViewAdapter
	extends md5d57e88b522f8a87d44d2b64e786daad3.ArrayRecyclerAdapter
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_getItemCount:()I:GetGetItemCountHandler\n" +
			"n_onBindViewHolder:(Landroid/support/v7/widget/RecyclerView$ViewHolder;I)V:GetOnBindViewHolder_Landroid_support_v7_widget_RecyclerView_ViewHolder_IHandler\n" +
			"n_onViewAttachedToWindow:(Landroid/support/v7/widget/RecyclerView$ViewHolder;)V:GetOnViewAttachedToWindow_Landroid_support_v7_widget_RecyclerView_ViewHolder_Handler\n" +
			"n_onViewDetachedFromWindow:(Landroid/support/v7/widget/RecyclerView$ViewHolder;)V:GetOnViewDetachedFromWindow_Landroid_support_v7_widget_RecyclerView_ViewHolder_Handler\n" +
			"";
		mono.android.Runtime.register ("DS.Kids.Apps.Droid.Controls.HighlightRecyclerViewAdapter, DS.Kids.Apps.Droid, Version=2.0.5.0, Culture=neutral, PublicKeyToken=null", HighlightRecyclerViewAdapter.class, __md_methods);
	}


	public HighlightRecyclerViewAdapter () throws java.lang.Throwable
	{
		super ();
		if (getClass () == HighlightRecyclerViewAdapter.class)
			mono.android.TypeManager.Activate ("DS.Kids.Apps.Droid.Controls.HighlightRecyclerViewAdapter, DS.Kids.Apps.Droid, Version=2.0.5.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public int getItemCount ()
	{
		return n_getItemCount ();
	}

	private native int n_getItemCount ();


	public void onBindViewHolder (android.support.v7.widget.RecyclerView.ViewHolder p0, int p1)
	{
		n_onBindViewHolder (p0, p1);
	}

	private native void n_onBindViewHolder (android.support.v7.widget.RecyclerView.ViewHolder p0, int p1);


	public void onViewAttachedToWindow (android.support.v7.widget.RecyclerView.ViewHolder p0)
	{
		n_onViewAttachedToWindow (p0);
	}

	private native void n_onViewAttachedToWindow (android.support.v7.widget.RecyclerView.ViewHolder p0);


	public void onViewDetachedFromWindow (android.support.v7.widget.RecyclerView.ViewHolder p0)
	{
		n_onViewDetachedFromWindow (p0);
	}

	private native void n_onViewDetachedFromWindow (android.support.v7.widget.RecyclerView.ViewHolder p0);

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

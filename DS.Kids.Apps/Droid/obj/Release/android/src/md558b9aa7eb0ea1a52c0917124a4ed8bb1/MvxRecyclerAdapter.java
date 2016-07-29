package md558b9aa7eb0ea1a52c0917124a4ed8bb1;


public class MvxRecyclerAdapter
	extends android.support.v7.widget.RecyclerView.Adapter
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_getItemCount:()I:GetGetItemCountHandler\n" +
			"n_onBindViewHolder:(Landroid/support/v7/widget/RecyclerView$ViewHolder;I)V:GetOnBindViewHolder_Landroid_support_v7_widget_RecyclerView_ViewHolder_IHandler\n" +
			"n_onCreateViewHolder:(Landroid/view/ViewGroup;I)Landroid/support/v7/widget/RecyclerView$ViewHolder;:GetOnCreateViewHolder_Landroid_view_ViewGroup_IHandler\n" +
			"n_onViewAttachedToWindow:(Landroid/support/v7/widget/RecyclerView$ViewHolder;)V:GetOnViewAttachedToWindow_Landroid_support_v7_widget_RecyclerView_ViewHolder_Handler\n" +
			"n_onViewDetachedFromWindow:(Landroid/support/v7/widget/RecyclerView$ViewHolder;)V:GetOnViewDetachedFromWindow_Landroid_support_v7_widget_RecyclerView_ViewHolder_Handler\n" +
			"";
		mono.android.Runtime.register ("Cirrious.MvvmCross.Droid.RecyclerView.MvxRecyclerAdapter, DS.Kids.Apps.Droid, Version=2.0.5.0, Culture=neutral, PublicKeyToken=null", MvxRecyclerAdapter.class, __md_methods);
	}


	public MvxRecyclerAdapter () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MvxRecyclerAdapter.class)
			mono.android.TypeManager.Activate ("Cirrious.MvvmCross.Droid.RecyclerView.MvxRecyclerAdapter, DS.Kids.Apps.Droid, Version=2.0.5.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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


	public android.support.v7.widget.RecyclerView.ViewHolder onCreateViewHolder (android.view.ViewGroup p0, int p1)
	{
		return n_onCreateViewHolder (p0, p1);
	}

	private native android.support.v7.widget.RecyclerView.ViewHolder n_onCreateViewHolder (android.view.ViewGroup p0, int p1);


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

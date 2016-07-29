using Android.Content;
using Android.Util;
using Android.Views;

using Cirrious.MvvmCross.Binding.Droid.Views;
using System.Collections.Generic;
using Cirrious.MvvmCross.Droid.RecyclerView;
using Cirrious.MvvmCross.Binding.ExtensionMethods;

namespace DS.Kids.Apps.Droid
{
	public class BRFXSectionRecyclerAdapter : MvxRecyclerAdapter
    {

		public BRFXSectionRecyclerAdapter(Context context)
            : base(context)
        {
        }

        public int HeaderItemTemplateId { get; set; }

        public int FooterItemTemplateId { get; set; }


		protected override View InflateViewForHolder(ViewGroup parent, int viewType, IMvxAndroidBindingContext bindingContext)
        {
			return base.InflateViewForHolder(parent, SelectTemplate(ItemsSource.GetPosition(), viewType), bindingContext);
        }

        public override int ViewTypeCount
        {
            get
            {
                return 3;
            }
        }

        public override int GetItemViewType(int position)
        {
            if (HeaderItemPositions.Contains(position) && HeaderItemTemplateId != 0)
            {
                return 1;
            }
            else if (FooterItemPositions.Contains(position) && FooterItemTemplateId != 0)
            {
                return 2;
            }

            return 0;
        }

        protected virtual int SelectTemplate(int position, int defaultTemplateId)
        {
            if (HeaderItemPositions.Contains(position) && HeaderItemTemplateId != 0)
            {
                return HeaderItemTemplateId;
            }
            else if (FooterItemPositions.Contains(position) && FooterItemTemplateId != 0)
            {
                return FooterItemTemplateId;
            }

            return defaultTemplateId;
        }

        public static void GetAttributes(BRFXSectionListAdapter sectionListAdapter, IAttributeSet attrs)
        {
            if (sectionListAdapter == null)
            {
                return;
            }

            var a = sectionListAdapter.Context.ObtainStyledAttributes(attrs, Resource.Styleable.BRFXSectionListView);
            var n = a.IndexCount;
            for (var i = 0; i < n; ++i)
            {
                var attr = a.GetIndex(i);
                if (attr == Resource.Styleable.BRFXSectionListView_HeaderItemTemplate)
                {
                    sectionListAdapter.HeaderItemTemplateId = a.GetResourceId(attr, 0);
                }
                else if (attr == Resource.Styleable.BRFXSectionListView_FooterItemTemplate)
                {
                    sectionListAdapter.FooterItemTemplateId = a.GetResourceId(attr, 0);
                }
            }



            a.Recycle();
        }

    }

}

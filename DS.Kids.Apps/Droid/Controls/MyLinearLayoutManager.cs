using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;

namespace DS.Kids.Apps.Droid.Controls
{

	public class MyLinearLayoutManager : LinearLayoutManager
	{
		#region Fields

		private readonly int[] _mMeasuredDimension = new int[2];

		#endregion

		#region Constructors and Destructors

		public MyLinearLayoutManager(Context context, int orientation, bool reverseLayout)
			: base(context, orientation, reverseLayout)
		{
		}

		#endregion

		#region Public Methods and Operators

		public override void OnMeasure(RecyclerView.Recycler recycler, RecyclerView.State state,
										int widthSpec, int heightSpec)
		{
			var widthMode = View.MeasureSpec.GetMode(widthSpec);
			var heightMode = View.MeasureSpec.GetMode(heightSpec);
			var widthSize = View.MeasureSpec.GetSize(widthSpec);
			var heightSize = View.MeasureSpec.GetSize(heightSpec);
			int width = 0;
			int height = 0;
			for(int i = 0; i < ItemCount; i++)
			{
				MeasureScrapChild(recycler, i,
					View.MeasureSpec.MakeMeasureSpec(i, MeasureSpecMode.Unspecified),
					View.MeasureSpec.MakeMeasureSpec(i, MeasureSpecMode.Unspecified));

				if(Orientation == Horizontal)
				{
					width = width + _mMeasuredDimension[0];
					if(i == 0)
					{
						height = _mMeasuredDimension[1];
					}
				}
				else
				{
					height = height + _mMeasuredDimension[1];
					if(i == 0)
					{
						width = _mMeasuredDimension[0];
					}
				}
			}
			switch(widthMode)
			{
				case MeasureSpecMode.Exactly:
					width = widthSize;
					break;
				case MeasureSpecMode.AtMost:
				case MeasureSpecMode.Unspecified:
					break;
			}

			switch(heightMode)
			{
				case MeasureSpecMode.Exactly:
					height = heightSize;
					break;
				case MeasureSpecMode.AtMost:
				case MeasureSpecMode.Unspecified:
					break;
			}

			SetMeasuredDimension(width, height);
		}

		#endregion

		#region Methods

		private void MeasureScrapChild(RecyclerView.Recycler recycler, int position, int widthSpec,
										int heightSpec)
		{
			View view = recycler.GetViewForPosition(position);
			if(view != null)
			{
				var p = (ViewGroup.MarginLayoutParams)view.LayoutParameters;
				int childWidthSpec = ViewGroup.GetChildMeasureSpec(widthSpec,
					PaddingLeft + PaddingRight, p.Width);
				int childHeightSpec = ViewGroup.GetChildMeasureSpec(heightSpec,
					PaddingTop + PaddingBottom, p.Height);
				view.Measure(childWidthSpec, childHeightSpec);
				_mMeasuredDimension[0] = view.MeasuredWidth + p.LeftMargin + p.RightMargin;
				_mMeasuredDimension[1] = view.MeasuredHeight + p.BottomMargin + p.TopMargin;
				recycler.RecycleView(view);
			}
		}

		#endregion
	}

}

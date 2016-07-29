using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Cirrious.MvvmCross.Binding.Droid.Views;

namespace DS.Kids.Apps.Droid.Controls
{
    [Register("ds.kids.apps.droid.controls.ResizableMvxImageView")]
    public class ResizableMvxImageView : MvxImageView
    {

        public ResizableMvxImageView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }

        public ResizableMvxImageView(Context context)
            : base(context)
        {
        }

        protected ResizableMvxImageView(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            var d = Drawable;

            if (d != null)
            {
                // ceil not round - avoid thin vertical gaps along the left/right edges
                int width = MeasureSpec.GetSize(widthMeasureSpec);
                int height = (int)Math.Ceiling(width * (float)d.IntrinsicHeight / d.IntrinsicWidth);
                SetMeasuredDimension(width, height);
            }
            else
            {
                base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            }
        }
    }
}
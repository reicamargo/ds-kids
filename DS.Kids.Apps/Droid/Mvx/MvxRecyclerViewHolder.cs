using System;
using System.Windows.Input;

using Android.Views;

using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;

namespace Cirrious.MvvmCross.Droid.RecyclerView
{

	public class MvxRecyclerViewHolder : Android.Support.V7.Widget.RecyclerView.ViewHolder, IMvxRecyclerViewHolder
	{
		#region Fields

		private readonly IMvxBindingContext _bindingContext;

		private object _cachedDataContext;

		private ICommand _click, _longClick;

		private bool _clickOverloaded, _longClickOverloaded;

		#endregion

		#region Constructors and Destructors

		public MvxRecyclerViewHolder(View itemView, IMvxAndroidBindingContext context)
			: base(itemView)
		{
			_bindingContext = context;
		}

		#endregion

		#region Public Properties

		public IMvxBindingContext BindingContext
		{
			get
			{
				return _bindingContext;
			}
			set
			{
				throw new NotImplementedException("BindingContext is readonly in the list item");
			}
		}

		public ICommand Click
		{
			get
			{
				return _click;
			}
			set
			{
				_click = value;
				if(_click != null) { EnsureClickOverloaded(); }
			}
		}

		public object DataContext
		{
			get
			{
				return _bindingContext.DataContext;
			}
			set
			{
				_bindingContext.DataContext = value;
			}
		}

		public ICommand LongClick
		{
			get
			{
				return _longClick;
			}
			set
			{
				_longClick = value;
				if(_longClick != null) { EnsureLongClickOverloaded(); }
			}
		}

		#endregion

		#region Public Methods and Operators

		public void OnAttachedToWindow()
		{
			if(_cachedDataContext != null && DataContext == null)
			{
				DataContext = _cachedDataContext;
			}
		}

		public void OnDetachedFromWindow()
		{
			_cachedDataContext = DataContext;
			DataContext = null;
		}

		#endregion

		#region Methods

		protected override void Dispose(bool disposing)
		{
			if(disposing)
			{
				_bindingContext.ClearAllBindings();
				_cachedDataContext = null;
			}

			base.Dispose(disposing);
		}

		protected virtual void ExecuteCommandOnItem(ICommand command)
		{
			if(command == null)
			{
				return;
			}

			var item = DataContext;
			if(item == null)
			{
				return;
			}

			if(!command.CanExecute(item))
			{
				return;
			}

			command.Execute(item);
		}

		private void EnsureClickOverloaded()
		{
			if(_clickOverloaded)
			{
				return;
			}
			_clickOverloaded = true;
			ItemView.Click += (sender, args) => ExecuteCommandOnItem(Click);
		}

		private void EnsureLongClickOverloaded()
		{
			if(_longClickOverloaded)
			{
				return;
			}
			_longClickOverloaded = true;
			ItemView.LongClick += (sender, args) => ExecuteCommandOnItem(LongClick);
		}

		#endregion
	}

}

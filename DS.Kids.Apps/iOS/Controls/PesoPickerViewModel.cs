using System;
using System.Collections;
using System.Collections.Specialized;
using System.Windows.Input;

using Cirrious.CrossCore;
using Cirrious.CrossCore.WeakSubscription;
using Cirrious.MvvmCross.Binding.Attributes;
using Cirrious.MvvmCross.Binding.ExtensionMethods;

using UIKit;

namespace DS.Kids.Apps.iOS.Controls
{

	public class PesoPickerViewModel
		: UIPickerViewModel
	{

		private IEnumerable _itemsSource;

		private nint _selectedComponent = 0;

		private object _selectedItem;

		private IDisposable _subscription;

		private readonly UIPickerView _pickerView;

		public PesoPickerViewModel(UIPickerView pickerView)
		{
			_pickerView = pickerView;
			ReloadOnAllItemsSourceSets = true;
		}

		public bool ReloadOnAllItemsSourceSets { get; set; }

		[MvxSetToNullAfterBinding]
		public virtual IEnumerable ItemsSource
		{
			get
			{
				return _itemsSource;
			}
			set
			{
				if(ReferenceEquals(_itemsSource, value)
					&& !ReloadOnAllItemsSourceSets)
				{
					return;
				}

				if(_subscription != null)
				{
					_subscription.Dispose();
					_subscription = null;
				}

				_itemsSource = value;

				var collectionChanged = _itemsSource as INotifyCollectionChanged;
				if(collectionChanged != null)
				{
					_subscription = collectionChanged.WeakSubscribe(CollectionChangedOnCollectionChanged);
				}

				Reload();
				ShowSelectedItem();
			}
		}

		public decimal SelectedItem
		{
			get
			{
				return Convert.ToInt32(_selectedItem) + Convert.ToInt32(_selectedComponent) / 10.0M;
			}
			set
			{
				_selectedComponent = (nint)((value * 10) % 10);
				_selectedItem = value;

				ShowSelectedItem();
			}
		}

		public ICommand SelectedChangedCommand { get; set; }

		protected override void Dispose(bool disposing)
		{
			if(disposing)
			{
				if(_subscription != null)
				{
					_subscription.Dispose();
					_subscription = null;
				}
			}

			base.Dispose(disposing);
		}

		protected virtual void CollectionChangedOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			Mvx.Trace(
				"CollectionChanged called inside PesoPickerViewModel - beware that this isn't fully tested - picker might not fully support changes while the picker is visible");
			Reload();
		}

		protected virtual void Reload()
		{
			_pickerView.ReloadComponent(0);
		}

		public override nint GetComponentCount(UIPickerView picker)
		{
			return 4;
		}

		public override nint GetRowsInComponent(UIPickerView picker, nint component)
		{
			if(component == 0)
			{
				return _itemsSource == null ? 0 : _itemsSource.Count();
			}

			if(component == 1 || component == 3)
			{
				return 1;
			}

			return 10;
		}

		public override string GetTitle(UIPickerView picker, nint row, nint component)
		{
			return _itemsSource == null ? "-" : RowTitle(row, component, _itemsSource.ElementAt((int)row));
		}

		protected virtual string RowTitle(nint row, nint component, object item)
		{
			if(component == 0)
			{
				var decimalItem = (decimal)item;
				return decimalItem.ToString("0");
			}

			if(component == 1)
			{
				return ",";
			}

			if(component == 3)
			{
				return "kg";
			}

			return ((int)row).ToString();
		}

		public override void Selected(UIPickerView picker, nint row, nint component)
		{
			if(component == 0)
			{
				_selectedItem = _itemsSource.ElementAt((int)row);
			}
			else if(component == 2)
			{
				_selectedComponent = row;
			}

			var handler = SelectedItemChanged;
			if(handler != null)
			{
				handler(this, EventArgs.Empty);
			}

			var command = SelectedChangedCommand;
			if(command != null)
			{
				if(command.CanExecute(_selectedItem))
				{
					command.Execute(_selectedItem);
				}
			}
		}

		// ReSharper disable once EventNeverSubscribedTo.Global
		public event EventHandler SelectedItemChanged;

		protected virtual void ShowSelectedItem()
		{
			if(_itemsSource == null)
			{
				return;
			}

			var position = _itemsSource.GetPosition(_selectedItem);
			if(position < 0)
			{
				return;
			}

			var animated = !_pickerView.Hidden;
			_pickerView.Select(position, _selectedComponent, animated);
		}

	}

}

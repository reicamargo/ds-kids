using System.Collections.Generic;

using Android.OS;
using Android.Views;

using BRFX.Core.Droid.Views;
using BRFX.Core.Validation;

using Cirrious.MvvmCross.Binding.Droid.BindingContext;

using DS.Kids.Apps.Core.ViewModels;

using Debug = System.Diagnostics.Debug;

namespace DS.Kids.Apps.Droid.Views
{

	internal class AlterarSenhaView : FormBaseView
	{
		#region Public Methods and Operators

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			if(item.ItemId == Resource.Id.alterarSenhaMenu_salvarSenha)
			{
				var alterarSenhaViewModel = ViewModel as AlterarSenhaViewModel;
				if(alterarSenhaViewModel != null)
				{
					alterarSenhaViewModel.SalvarCommand.Execute();
				}
			}

			return base.OnOptionsItemSelected(item);
		}

		public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
		{
			inflater.Inflate(Resource.Menu.alterarSenhaMenu, menu);

			base.OnCreateOptionsMenu(menu, inflater);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var ignored = base.OnCreateView(inflater, container, savedInstanceState);

			var view = this.BindingInflate(Resource.Layout.AlterarSenhaView, null);

			var alterarSenhaViewModel = ViewModel as AlterarSenhaViewModel;
			if(alterarSenhaViewModel != null)
			{
				Title = alterarSenhaViewModel.Titulo;
			}

			return view;
		}

		public override void OnPrepareOptionsMenu(IMenu menu)
		{
			for(var i = 0; i < menu.Size(); i++)
			{
				var menuItem = menu.GetItem(i);
				if(menuItem.ItemId == Resource.Id.alterarSenhaMenu_salvarSenha)
				{
					menuItem.SetVisible(true);
				}
				else
				{
					menuItem.SetVisible(false);
				}
			}

			base.OnPrepareOptionsMenu(menu);
		}

		#endregion

		#region Methods

		protected override void HandleValidation(object sender, Dictionary<string, List<ValidationAttribute>> validationErrors)
		{
			foreach(var validationError in validationErrors)
			{
				if(validationError.Value != null)
				{
					foreach(var validationAttribute in validationError.Value)
					{
						Debug.WriteLine(validationAttribute.ValidationMessage);
					}
				}
			}
		}

		#endregion
	}

}

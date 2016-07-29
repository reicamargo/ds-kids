using System.Collections.Generic;

using Android.OS;
using Android.Views;

using BRFX.Core.Droid.Validation;
using BRFX.Core.Droid.Views;
using BRFX.Core.Validation;

using Cirrious.MvvmCross.Binding.Droid.BindingContext;

using DS.Kids.Apps.Core.ViewModels;

using Debug = System.Diagnostics.Debug;

namespace DS.Kids.Apps.Droid.Views
{

	internal class AlterarFilhoView : FormBaseView
	{
		#region Public Methods and Operators

		public AlterarFilhoView()
		{
			IsActionBarHomeView = true;
		}

		public override void ConfigureActionBarView(View view)
		{
			base.ConfigureActionBarView(view);

			Title = "Alterar filho";
		}

		public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
		{
			inflater.Inflate (Resource.Menu.alterarSenhaMenu, menu);

			base.OnCreateOptionsMenu (menu, inflater);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var ignored = base.OnCreateView(inflater, container, savedInstanceState);

			var view = this.BindingInflate(Resource.Layout.AlterarFilhoView, null);

			var dataNascEditText = view.FindViewById<BRFXEditText>(Resource.Id.alterarFilho_DataNascimentoEditText);

			dataNascEditText.AddDatePopup<AlterarFilhoView, AlterarFilhoViewModel>(this, vm => vm.DataNascimento, "DateFormat");

			return view;
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

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			if(item.ItemId == Resource.Id.alterarSenhaMenu_salvarSenha)
			{
				var alterarFilhoViewModel = ViewModel as AlterarFilhoViewModel;
				if(alterarFilhoViewModel != null)
				{
					if (alterarFilhoViewModel.SelecionarOuSalvarCommand.CanExecute()) {
						alterarFilhoViewModel.SelecionarOuSalvarCommand.Execute();
					}
				}
			}

			return base.OnOptionsItemSelected(item);
		}

		public override void OnPrepareOptionsMenu(IMenu menu)
		{
			for(var i = 0; i < menu.Size(); i++)
			{
				var menuItem = menu.GetItem(i);
				menuItem.SetVisible(menuItem.ItemId == Resource.Id.alterarSenhaMenu_salvarSenha);
			}

			base.OnPrepareOptionsMenu(menu);
		}

		#endregion
	}

}

using System.Collections.Generic;

using Android.OS;
using Android.Views;

using BRFX.Core.Droid.Views;
using BRFX.Core.Validation;

using Cirrious.MvvmCross.Binding.Droid.BindingContext;

using Debug = System.Diagnostics.Debug;

namespace DS.Kids.Apps.Droid.Views
{

	internal class AtualizarPesoAlturaView : FormBaseView
	{
		#region Public Methods and Operators

		public override void ConfigureActionBarView(View view)
		{
			HideActionBar();

			base.ConfigureActionBarView(view);

			Title = "Atualizar Peso e Altura";
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var ignored = base.OnCreateView(inflater, container, savedInstanceState);

			var view = this.BindingInflate(Resource.Layout.AtualizarPesoAlturaView, null);

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

		#endregion
	}

}

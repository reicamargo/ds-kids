using System.Collections.Generic;

using Android.OS;
using Android.Views;

using BRFX.Core.Droid.Views;
using BRFX.Core.Validation;

using Cirrious.MvvmCross.Binding.Droid.BindingContext;

using Debug = System.Diagnostics.Debug;

namespace DS.Kids.Apps.Droid.Views
{
	class CriarContaView : FormBaseView
	{
		#region Public Methods and Operators

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var ignored = base.OnCreateView(inflater, container, savedInstanceState);

			var view = this.BindingInflate(Resource.Layout.CriarContaView, null);

			return view;
		}

		public override void ConfigureActionBarView(View view)
		{
			base.ConfigureActionBarView(view);

			Title = "Criar conta";
		}

		#endregion

		protected override void HandleValidation(object sender, Dictionary<string, List<ValidationAttribute>> validationErrors)
		{
			foreach (var validationError in validationErrors)
			{
				if (validationError.Value != null)
				{
					foreach (var validationAttribute in validationError.Value)
					{
						Debug.WriteLine(validationAttribute.ValidationMessage);
					}
				}
			}
		}
	}
}
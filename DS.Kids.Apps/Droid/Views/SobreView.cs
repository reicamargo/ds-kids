using System;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using System.Threading.Tasks;
using System.Linq;
using DS.Kids.Model;
using DS.Kids.Model.Validations;
using DS.Kids.Model.Communication;
using Java.Net;

namespace DS.Kids.Apps.Droid.Views
{

	internal class SobreView : BaseHomeChildView
	{
		#region Constructors and Destructors

		public SobreView()
		{
			IsActionBarHomeView = true;
		}

		#endregion

		#region Public Methods and Operators

		public override void ConfigureActionBarView(View view)
		{
			base.ConfigureActionBarView(view);

			Title = "Sobre";
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var ignored = base.OnCreateView(inflater, container, savedInstanceState);

			ShowActionBar();

			var view = this.BindingInflate(Resource.Layout.SobreView, null);
            
            return view;
		}

        //private async void LoadImageView(View view)
        //{
        //    var communication = new Model.Communication.Parceiros();
        //    var result = await communication.ListarPorTipoAsync(TipoParceiro.Patrocinador);

        //    if (result.ResultCode != ResultCodes.Success)
        //        return;

        //    var parceiro = result.Data.FirstOrDefault();
        //    var imageView = view.FindViewById<ImageView>(Resource.Id.imagemParceiro);
        //    var urlImagem = string.Format("{0}{1}", Endpoints.BASE, parceiro.UrlImagem);
        //    var bitmap = await GetBitmapfromUrl(urlImagem);
        //    imageView.SetImageBitmap(bitmap);
        //}

        private async Task<Bitmap> GetBitmapfromUrl(string imageUrl)
        {
            try
            {
                var url = new URL(imageUrl);
                var connection = (HttpURLConnection)url.OpenConnection();
                connection.DoInput = true;
                await connection.ConnectAsync();
                return await BitmapFactory.DecodeStreamAsync(connection.InputStream);
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }

}

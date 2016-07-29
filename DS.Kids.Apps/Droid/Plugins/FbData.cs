using System.Threading.Tasks;

using Android.OS;

using Cirrious.CrossCore;

using DS.Kids.Apps.Core.Plugins;
using DS.Kids.Model;

using Java.Lang;

using Xamarin.Facebook;

using Debug = System.Diagnostics.Debug;
using Exception = System.Exception;

namespace DS.Kids.Apps.Droid.Plugins
{

	public class FbData : Object, IFbData, GraphRequest.IGraphJSONObjectCallback
	{
		#region Fields

		private LoginSocial _loginSocial;

		#endregion

		#region Public Methods and Operators

		public async Task<LoginSocial> GetUserInfoAsync()
		{
			_loginSocial = null;
			try
			{
				await Task.Run(() =>
					{
						var request = GraphRequest.NewMeRequest(AccessToken.CurrentAccessToken, this);
						var parameters = new Bundle();
						parameters.PutString("fields", "id,name,email");
						request.Parameters = parameters;
						request.ExecuteAndWait();
					});
				return _loginSocial;
			}
			catch(Exception ex)
			{
				Debug.WriteLine(ex);
			}

			return null;
		}

		public void Initialize()
		{
			Mvx.RegisterSingleton<IFbData>(this);
		}

		public void OnCompleted(Org.Json.JSONObject user, GraphResponse response)
		{
			if(user != null)
			{
				_loginSocial = new LoginSocial
									{
										Chave = user.OptString("id"),
										Nome = user.OptString("name"),
										Email = user.OptString("email"),
										RedeSocial = RedesSociais.Facebook
									};
			}
			else
			{
				Debug.WriteLine("Failed to get 'me'!");
			}
		}

		#endregion
	}

}

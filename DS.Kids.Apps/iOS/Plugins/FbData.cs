using System;
using System.Threading.Tasks;

using Cirrious.CrossCore;

using DS.Kids.Apps.Core.Plugins;
using DS.Kids.Model;
using Facebook.CoreKit;
using Foundation;

namespace DS.Kids.Apps.iOS.Plugins
{

	public class FbData : IFbData
	{

		public void Initialize()
		{
			Mvx.RegisterSingleton<IFbData>(this);
		}

		public Task<LoginSocial> GetUserInfoAsync()
		{
		    try
		    {
                var tcs = new TaskCompletionSource<LoginSocial>();

                var parameters = NSDictionary.FromObjectAndKey((NSString)"id,name,email", (NSString)"fields");
                var fbRequestResult = new GraphRequest("me", parameters);
		        fbRequestResult.Start((connection, result, error) =>
		        {
		            try
		            {
                        var id = (NSString)((NSDictionary)result)["id"];
                        if (id != null)
                        {
                            var name = (NSString)((NSDictionary)result)["name"];
                            var email = (NSString)((NSDictionary)result)["email"];
                            tcs.TrySetResult(new LoginSocial
                            {
                                Chave = id,
                                Nome = name,
                                Email = email,
                                RedeSocial = RedesSociais.Facebook
                            });
                        }
                        tcs.TrySetException(new Exception("Id not found."));
		            }
		            catch (OperationCanceledException)
		            {
		                tcs.TrySetCanceled();
		            }
		            catch (Exception exc)
		            {
		                tcs.TrySetException(exc);
		            }
		        });

                return tcs.Task;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
			}

			System.Diagnostics.Debug.WriteLine("Failed to get 'me'!");

			return null;
		}
	}
}

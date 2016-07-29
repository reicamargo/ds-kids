using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace DS.Kids.Model.Communication.Support
{
	/// <summary>
	/// Api para comunicação rest
	/// </summary>
	public static class Rest
	{

		/// <summary>
		/// Efetua um Get de forma assíncrona
		/// </summary>
		/// <typeparam name="TResult">Tipo do Retorno</typeparam>
		/// <param name="url">Url da Api</param>
		/// <param name="cancellationToken">Token de cancelamento do request</param>
		/// <returns>Retorno da Api</returns>
		public static async Task<TResult> GetAsync<TResult>(string url, CancellationToken? cancellationToken = null)
		{
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
			{
				SetConfiguration(client);

				var result = await client.GetAsync(url, cancellationToken ?? CancellationToken.None);
				ValidateStatusCode(result.StatusCode, result.ReasonPhrase);
				var jsonString = await result.Content.ReadAsStringAsync();

				return JsonConvert.DeserializeObject<TResult>(jsonString);
			}
		}

		/// <summary>
		/// Efetua um Post de forma assíncrona
		/// </summary>
		/// <typeparam name="TResult">Tipo do Retorno</typeparam>
		/// <typeparam name="TContent">Tipo do conteúdo a ser enviado</typeparam>
		/// <param name="url">Url da Api</param>
		/// <param name="content">Conteúdo a ser enviado</param>
		/// <param name="cancellationToken">Token de cancelamento do request</param>
		/// <returns>Retorno da Api</returns>
		public static async Task<TResult> PostAsync<TResult, TContent>(string url, TContent content, CancellationToken? cancellationToken = null)
		{
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
			{
				SetConfiguration(client);

				var jsonContent = new JsonContent(content);
				var result = await client.PostAsync(url, jsonContent, cancellationToken ?? CancellationToken.None);
				var jsonString = await result.Content.ReadAsStringAsync();
				var json = JsonConvert.DeserializeObject<TResult>(jsonString);

				ValidateStatusCode(result.StatusCode, result.ReasonPhrase);
				
				return json;
			}
		}

		/// <summary>
		/// Efetua um Post de forma assíncrona
		/// </summary>
		/// <typeparam name="TResult">Tipo do Retorno</typeparam>
		/// <param name="url">Url da Api</param>
		/// <param name="cancellationToken">Token de cancelamento do request</param>
		/// <returns>Retorno da Api</returns>
		public static async Task<TResult> PostAsync<TResult>(string url, CancellationToken? cancellationToken = null)
		{
			return await PostAsync<TResult, object>(url, null, cancellationToken);
		}

		/// <summary>
		/// Efetua um Put de forma assíncrona
		/// </summary>
		/// <typeparam name="TResult">Tipo do Retorno</typeparam>
		/// <typeparam name="TContent">Tipo do conteúdo a ser enviado</typeparam>
		/// <param name="url">Url da Api</param>
		/// <param name="content">Conteúdo a ser enviado</param>
		/// <param name="cancellationToken">Token de cancelamento do request</param>
		/// <returns>Retorno da Api</returns>
		public static async Task<TResult> PutAsync<TResult, TContent>(string url, TContent content, CancellationToken? cancellationToken = null)
		{
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
			{
				SetConfiguration(client);

				var jsonContent = new JsonContent(content);
				var result = await client.PutAsync(url, jsonContent, cancellationToken ?? CancellationToken.None);
				var jsonString = await result.Content.ReadAsStringAsync();
				var json = JsonConvert.DeserializeObject<TResult>(jsonString);

				ValidateStatusCode(result.StatusCode, result.ReasonPhrase);

				return json;
			}
		}

		/// <summary>
		/// Efetua um Post de forma assíncrona
		/// </summary>
		/// <typeparam name="TResult">Tipo do Retorno</typeparam>
		/// <param name="url">Url da Api</param>
		/// <param name="cancellationToken">Token de cancelamento do request</param>
		/// <returns>Retorno da Api</returns>
		public static async Task<TResult> PutAsync<TResult>(string url, CancellationToken? cancellationToken = null)
		{
			return await PutAsync<TResult, object>(url, null, cancellationToken);
		}

		/// <summary>
		/// Efetua um Delete de forma assíncrona
		/// </summary>
		/// <typeparam name="TResult">Tipo do Retorno</typeparam>
		/// <param name="url">Url da Api</param>
		/// <param name="cancellationToken">Token de cancelamento do request</param>
		/// <returns>Retorno da Api</returns>
		public static async Task<TResult> DeleteAsync<TResult>(string url, CancellationToken? cancellationToken = null)
		{
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
			{
				SetConfiguration(client);

				var result = await client.DeleteAsync(url, cancellationToken ?? CancellationToken.None);
				var jsonString = await result.Content.ReadAsStringAsync();
				var json = JsonConvert.DeserializeObject<TResult>(jsonString);

				ValidateStatusCode(result.StatusCode, result.ReasonPhrase);

				return json;
			}
		}

		/// <summary>
		/// Seta configuração para comunicação com a api
		/// </summary>
		/// <param name="client">Cliente Http</param>
		private static void SetConfiguration(HttpClient client)
		{
			client.Timeout = new TimeSpan(0, 1, 0);
			var token = Authorization.Singleton.GetToken();
			if (!string.IsNullOrEmpty(token))
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			else
				client.DefaultRequestHeaders.Authorization = null;

            client.DefaultRequestHeaders.Add("Parceiro", "8");
        }

		/// <summary>
		/// Valida o StatusCode de cada comunicação com api.
		/// Isso é feito para retornar uma mensagem mais amigável em alguns tipos de retorno como o Fornidden e Timeout
		/// </summary>
		/// <param name="status">StatusCode retornado da comunicação com a API</param>
		/// <param name="reasonPhrase">ReasonPhrase retornado da comunicação com a API</param>
		private static void ValidateStatusCode(HttpStatusCode status, string reasonPhrase)
		{
			var message = Validations.Messages.Get(Validations.ResultCodes.Opsss);

			if (status == HttpStatusCode.Forbidden)
				message = Validations.Messages.Get(Validations.ResultCodes.AcessoNegado);

			if (status == HttpStatusCode.GatewayTimeout || status == HttpStatusCode.RequestTimeout)
				message = Validations.Messages.Get(Validations.ResultCodes.Timeout);

			if (status != HttpStatusCode.OK)
			{
				var ex = new Exception(message);
				ex.Data.Add("StatusCode", status);
				ex.Data.Add("ReasonPhrase", reasonPhrase);
				throw ex;
			}
		}
	}
}
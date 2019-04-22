using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace kcdnug.Services
{
	public class ServiceBase : IDisposable
	{

		protected static readonly Uri ApiBaseUri = new Uri("http://api.kcdnug.net/api/");

		private IHttpMessageHandlerFactory MessageHandlerFactory { get; }
		public HttpClient CurrentHttpClient { get; }

		public ServiceBase(IHttpMessageHandlerFactory messageHandlerFactory)
		{
			MessageHandlerFactory = messageHandlerFactory;
			CurrentHttpClient = new HttpClient(MessageHandlerFactory.GetHttpMessageHandler());
		}

		protected async Task<ApiResult<T>> GetApiData<T>(Uri apiUri, string version = "", bool authorized = true)
		{
			try
			{

				if (authorized)
				{
					//todo: add profile service to get current token
					//client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", LocalProfileService.GetAuthToken());
				}

				if (!string.IsNullOrEmpty(version))
				{
					var APIVersion = new MediaTypeWithQualityHeaderValue("application/json");
					APIVersion.Parameters.Add(new NameValueHeaderValue("version", version));
					CurrentHttpClient.DefaultRequestHeaders.Accept.Add(APIVersion);
				}

				HttpResponseMessage apiResult = await CurrentHttpClient.GetAsync(apiUri);
				if (apiResult.StatusCode == HttpStatusCode.Unauthorized) return await HandleUnauthorized<T>();

				string json = null;

				if (!apiResult.IsSuccessStatusCode)
				{
					return new ApiResult<T>()
					{
						StatusCode = apiResult.StatusCode
					};
				}

				json = apiResult.Content.ReadAsStringAsync().Result;
				var result = JsonConvert.DeserializeObject<T>(json);
				return new ApiResult<T>
				{
					StatusCode = apiResult.StatusCode,
					Data = result
				};
			}
			catch (Exception)
			{
				return new ApiResult<T>
				{
					Data = default(T),
					StatusCode = HttpStatusCode.BadRequest
				};
			}
		}
		
		private async Task<ApiResult<T>> HandleUnauthorized<T>()
		{
			//todo: handle any login needed event messaging
			return await Task.FromResult(new ApiResult<T>()
			{
				StatusCode = HttpStatusCode.Unauthorized
			});
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					CurrentHttpClient?.Dispose();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~ServiceBase()
		// {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion
	}
}

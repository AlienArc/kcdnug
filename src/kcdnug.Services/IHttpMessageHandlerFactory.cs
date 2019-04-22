using System.Net.Http;

namespace kcdnug.Services
{
	public interface IHttpMessageHandlerFactory
	{
		HttpMessageHandler GetHttpMessageHandler();
	}

	public class HttpClientMessageHandlerFactory : IHttpMessageHandlerFactory
	{
		public HttpMessageHandler GetHttpMessageHandler()
		{
			return new HttpClientHandler();
		}
	}
}
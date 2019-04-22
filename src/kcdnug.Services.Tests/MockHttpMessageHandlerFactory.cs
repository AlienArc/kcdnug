using System.Net.Http;
using kcdnug.Services;

namespace Tests
{
	public class MockHttpMessageHandlerFactory : IHttpMessageHandlerFactory
	{
		public HttpMessageHandler GetHttpMessageHandler()
		{
			return new MockApiHttpMessageHandler();
		}

	}
}
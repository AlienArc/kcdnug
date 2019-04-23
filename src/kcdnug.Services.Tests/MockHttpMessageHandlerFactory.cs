using System.Net.Http;
using kcdnug.Services;

namespace Tests
{
	public class MockHttpMessageHandlerFactory : IHttpMessageHandlerFactory
	{
		public MockApiHttpMessageHandler MockApiHandler { get; }

		public MockHttpMessageHandlerFactory(MockApiHttpMessageHandler mockApiHandler = null)
		{
			MockApiHandler = mockApiHandler ?? new MockApiHttpMessageHandler();
		}

		public HttpMessageHandler GetHttpMessageHandler()
		{
			return MockApiHandler;
		}

	}
}
using kcdnug.Services;
using NUnit.Framework;

namespace Tests
{
	public class EventServiceTests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void GetUpcomingEventsReturnsListFromServer()
		{
			var mockHttpMessageHandler = new MockHttpMessageHandlerFactory();
			var eventService = new EventsService(mockHttpMessageHandler);
			var events = eventService.GetUpcomingEvents().Result;
			Assert.IsNotEmpty(events.Data);
		}

		[Test]
		public void GetUpcomingEventsReturnsStatusCodeFromServerOnNotOk()
		{
			var mockHttpMessageHandler = new MockHttpMessageHandlerFactory();
			mockHttpMessageHandler.MockApiHandler.ReturnInternalServerError = true;
			var eventService = new EventsService(mockHttpMessageHandler);
			var events = eventService.GetUpcomingEvents().Result;

			Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, events.StatusCode);
		}

		[Test]
		public void GetUpcomingEventsReturnsUnauthorizedFromServerOnUnauthorized()
		{
			var mockHttpMessageHandler = new MockHttpMessageHandlerFactory();
			mockHttpMessageHandler.MockApiHandler.ReturnUnauthorized = true;
			var eventService = new EventsService(mockHttpMessageHandler);
			var events = eventService.GetUpcomingEvents().Result;

			Assert.AreEqual(System.Net.HttpStatusCode.Unauthorized, events.StatusCode);
		}
	}
}
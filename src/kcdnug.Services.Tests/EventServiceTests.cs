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
		public void GetUpcomingEventsList()
		{
			var mockHttpMessageHandler = new MockHttpMessageHandlerFactory();
			var eventService = new EventService(mockHttpMessageHandler);
			var events = eventService.GetUpcomingEvents().Result;
			Assert.IsNotEmpty(events.Data);
		}
	}
}
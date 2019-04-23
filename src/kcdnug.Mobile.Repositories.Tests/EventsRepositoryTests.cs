using kcdnug.Mobile.Repositories;
using kcdnug.Services;
using kcdnug.Services.Dtos;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
	public class EventsRepositoryTests
	{
		[SetUp]
		public void Setup()
		{
			AutoMapperInitialize.Initialize();
		}

		[Test]
		public void GetUpcomingEventsReturnsEventsFromService()
		{
			var eventsService = new Mock<IEventsService>();
			eventsService.Setup(s => s.GetUpcomingEvents())
				.Returns(() => Task.FromResult(new ApiResult<IList<EventSummaryDto>>
				{
					StatusCode = System.Net.HttpStatusCode.OK,
					Data = new List<EventSummaryDto>
					{
						new EventSummaryDto()
					}
				}));
			var eventsRepository = new EventsRepository(eventsService.Object);
			var upcomingEventsResult = eventsRepository.GetUpcomingEvents().Result;
			Assert.IsNotEmpty(upcomingEventsResult.Data);
		}
		
		[Test]
		public void GetUpcomingEventsReturnsExpectedEventTitle()
		{
			var eventName = "Event 1";
			var eventsService = new Mock<IEventsService>();
			eventsService.Setup(s => s.GetUpcomingEvents())
				.Returns(() => Task.FromResult(new ApiResult<IList<EventSummaryDto>>
				{
					StatusCode = System.Net.HttpStatusCode.OK,
					Data = new List<EventSummaryDto>
					{
						new EventSummaryDto() {Title = eventName}
					}
				}));
			var eventsRepository = new EventsRepository(eventsService.Object);
			var upcomingEventsResult = eventsRepository.GetUpcomingEvents().Result;

			Assert.AreEqual(eventName, upcomingEventsResult.Data[0].Title);
		}

		[Test]
		public void GetUpcomingEventsReturnsOnlineWithSericeSuccess()
		{
			var eventsService = new Mock<IEventsService>();
			eventsService.Setup(s => s.GetUpcomingEvents())
				.Returns(() => Task.FromResult(new ApiResult<IList<EventSummaryDto>>
				{
					StatusCode = System.Net.HttpStatusCode.OK
				}));
			var eventsRepository = new EventsRepository(eventsService.Object);
			var upcomingEventsResult = eventsRepository.GetUpcomingEvents().Result;

			Assert.AreEqual(RepositoryResultStatus.Online, upcomingEventsResult.Status);
		}

		[Test]
		public void GetUpcomingEventsReturnsNoDataWithSericeFailure()
		{
			var eventsService = new Mock<IEventsService>();
			eventsService.Setup(s => s.GetUpcomingEvents())
				.Returns(() => Task.FromResult(new ApiResult<IList<EventSummaryDto>>
				{
					StatusCode = System.Net.HttpStatusCode.RequestTimeout
				}));
			var eventsRepository = new EventsRepository(eventsService.Object);
			var upcomingEventsResult = eventsRepository.GetUpcomingEvents().Result;

			Assert.AreEqual(RepositoryResultStatus.NoData, upcomingEventsResult.Status);
		}
	}
}
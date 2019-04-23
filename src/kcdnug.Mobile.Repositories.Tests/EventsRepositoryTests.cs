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
		public void GetUpcomingEvents()
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
	}
}
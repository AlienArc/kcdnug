using kcdnug.Mobile.Repositories.Models;
using kcdnug.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kcdnug.Mobile.Repositories
{
	public class EventsRepository : IEventsRepository
	{
		private IEventsService EventsService { get; }

		public EventsRepository(IEventsService eventsService)
		{
			EventsService = eventsService;
		}

		public async Task<RepositoryResult<IList<EventSummary>>> GetUpcomingEvents()
		{
			var serviceResults = await EventsService.GetUpcomingEvents();
			if (serviceResults.StatusCode != System.Net.HttpStatusCode.OK)
			{
				//TODO: Return Offline Cached Data

				return new RepositoryResult<IList<EventSummary>>
				{
					Status = RepositoryResultStatus.NoData
				};
			}

			return new RepositoryResult<IList<EventSummary>>
			{
				Status = RepositoryResultStatus.Online,
				Data = AutoMapper.Mapper.Map<IList<EventSummary>>(serviceResults.Data)
			};

			//TODO: Update Offline Cached Data

		}
	}
}

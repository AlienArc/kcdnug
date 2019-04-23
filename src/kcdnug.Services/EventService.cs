using kcdnug.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace kcdnug.Services
{

	public class EventsService : ServiceBase, IEventsService
	{

		public static readonly Uri UpcomingEventSummaryUri = new Uri(ServiceBase.ApiBaseUri, "events");

		public EventsService(IHttpMessageHandlerFactory messageHandlerFactory) : base(messageHandlerFactory)
		{
		}

		public async Task<ApiResult<IList<EventSummaryDto>>> GetUpcomingEvents()
		{
			return await GetApiData<IList<EventSummaryDto>>(UpcomingEventSummaryUri, "", false);
		}

	}
}

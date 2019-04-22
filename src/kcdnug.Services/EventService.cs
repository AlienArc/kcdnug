using kcdnug.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace kcdnug.Services
{

	public class EventService : ServiceBase
	{

		public static readonly Uri UpcomingEventSummaryUri = new Uri(ServiceBase.ApiBaseUri, "events");

		public EventService(IHttpMessageHandlerFactory messageHandlerFactory) : base(messageHandlerFactory)
		{
		}

		public async Task<ApiResult<IList<EventSummary>>> GetUpcomingEvents()
		{
			return await GetApiData<IList<EventSummary>>(UpcomingEventSummaryUri, "", false);
		}

	}
}

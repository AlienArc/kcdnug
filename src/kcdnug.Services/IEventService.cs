using System.Collections.Generic;
using System.Threading.Tasks;
using kcdnug.Services.Dtos;

namespace kcdnug.Services
{
	public interface IEventsService
	{
		Task<ApiResult<IList<EventSummaryDto>>> GetUpcomingEvents();
	}
}
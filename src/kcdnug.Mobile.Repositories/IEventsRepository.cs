using System.Collections.Generic;
using System.Threading.Tasks;
using kcdnug.Mobile.Repositories.Models;
using kcdnug.Services;

namespace kcdnug.Mobile.Repositories
{
	public interface IEventsRepository
	{
		Task<RepositoryResult<IList<EventSummary>>> GetUpcomingEvents();
	}
}
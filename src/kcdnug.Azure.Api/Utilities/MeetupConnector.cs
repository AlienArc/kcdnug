using System.Threading.Tasks;

namespace kcdnug.Azure.Api.Utilities
{
	public class MeetupConnector
	{
		public static Task<bool> ValidateCredentials(string credentials)
		{
			return Task.FromResult("ABCD".Equals(credentials));
		}
	}
}
using System.Net;

namespace kcdnug.Services
{
	public class ApiResult<T>
	{
		public HttpStatusCode StatusCode { get; set; }
		public T Data { get; set; }
	}
}
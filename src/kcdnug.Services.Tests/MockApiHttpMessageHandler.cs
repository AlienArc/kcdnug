using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using kcdnug.Services;
using kcdnug.Services.Dtos;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;

namespace Tests
{
	public class MockApiHttpMessageHandler : MockHttpMessageHandler
	{
		public const string ValidUsername = "user1";
		public const string ValidPassword = "Password.1";

		public MockApiHttpMessageHandler()
		{
			//SetupAuthApi();
			SetupEventsApi();
		}

		private void SetupEventsApi()
		{
			this.When(HttpMethod.Get, EventsService.UpcomingEventSummaryUri.ToString())
				.Respond(request =>
				{

					var mockEvents = new List<EventSummaryDto>()
					{
						new EventSummaryDto()
						{
							Id = Guid.Empty,
							Published = new DateTime(2019,1,1),
							LastUpdated = new DateTime(2019,1,2),
							Title = "New Year Party",
							Description = "Join us for a fun New Year's Celebration of coding!",
							Urls = new List<string>(),
							ThumbnailUrl = "http://kcdnug.net/",
							ImageUrls = new List<string>()
						},
						new EventSummaryDto()
						{
							Id = new Guid(0,0,0,0,0,0,0,0,0,0,1),
							Published = new DateTime(2019,2,1),
							LastUpdated = new DateTime(2019,2,1),
							Title = "Meeting two",
							Description = "It's our second meeting and we already ran out of creative titles...",
							Urls = new List<string>(),
							ThumbnailUrl = "http://kcdnug.net/",
							ImageUrls = new List<string>()
						}
					};

					var mockEventSummaryInfo = JsonConvert.SerializeObject(mockEvents);

					return new HttpResponseMessage(HttpStatusCode.OK)
					{
						Content = new StringContent(mockEventSummaryInfo, Encoding.UTF8, "application/json")
					};
				});
		}

		//private void SetupAuthApi()
		//{

		//	this.When(HttpMethod.Post, ApiEndpointInfo.GetLoginUri().ToString())
		//		.Respond(request =>
		//		{
		//			var data = request.Content.ReadAsStringAsync().Result;
		//			var dict = ParseQueryString(data);

		//			var validUserName = dict.Any(
		//				p => p.Key.Equals("username", StringComparison.CurrentCultureIgnoreCase) &&
		//					 p.Value.Equals(ValidUsername, StringComparison.CurrentCultureIgnoreCase));
		//			var validPassword = dict.Any(
		//				p => p.Key.Equals("password", StringComparison.CurrentCultureIgnoreCase) &&
		//					 p.Value.Equals(ValidPassword));

		//			if (validUserName && validPassword)
		//			{
		//				return new HttpResponseMessage(HttpStatusCode.OK)
		//				{
		//					Content = new StringContent(
		//							"{'access_token' : 'new_token', 'token_type' : 'new_refresh', 'expires_in' : 600}",
		//							Encoding.Unicode, "application/json")
		//				}
		//					;
		//			}

		//			return new HttpResponseMessage(HttpStatusCode.Unauthorized);
		//		});

		//	//this.When(HttpMethod.Get, ApiEndpointInfo.GetAccountInfoUri().ToString())
		//	//	.Respond(new StringContent(JsonConvert.SerializeObject(new UserInfo() { FirstName = "Admin", LastName = "User", UserName = "admin" })));

		//}

		private static Dictionary<string, string> ParseQueryString(string data)
		{
			var query = data.Substring(data.IndexOf('?') + 1);
			var pairs = query.Split('&');
			return pairs
				.Select(o => o.Split('='))
				.Where(items => items.Length == 2)
				.ToDictionary(pair => Uri.UnescapeDataString(pair[0]),
					pair => Uri.UnescapeDataString(pair[1]));
		}

	}
}
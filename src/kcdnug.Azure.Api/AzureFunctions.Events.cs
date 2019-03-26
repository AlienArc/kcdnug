using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using kcdnug.Azure.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace kcdnug.Azure.Api
{
	public static partial class AzureFunctions
	{
		[FunctionName("Events")]
		public static async Task<IActionResult> RunEvents([HttpTrigger(AuthorizationLevel.Function, "get", "post", "put", Route = null)] HttpRequest req,
			[Table("Events")] CloudTable eventsTable,
			ILogger log)
		{
			switch (req.Method)
			{
				case "GET":
					return await HandleEventGetRequest(eventsTable, log);
				case "POST":
					return await HandleEventPostRequest(req, eventsTable, log);
				case "PUT":
					return await HandleEventPutRequest(req, eventsTable, log);
				default:
					return new BadRequestObjectResult("Unknown request method");
			}
		}

		private static async Task<IActionResult> HandleEventPutRequest(HttpRequest req, CloudTable eventsTable, ILogger log)
		{
			var jsonString = await req.ReadAsStringAsync();
			var updatedEvent = JsonConvert.DeserializeObject<Event>(jsonString);
			var retrieveOperation = TableOperation.Retrieve<Event>(updatedEvent.PartitionKey, updatedEvent.RowKey);
			var retrievedResult = await eventsTable.ExecuteAsync(retrieveOperation);

			if (retrievedResult.HttpStatusCode != (int)HttpStatusCode.OK || !(retrievedResult.Result is Event existingEvent))
				return new BadRequestObjectResult("Could not retrieve event to update it");

			existingEvent.Description = updatedEvent.Description;
			existingEvent.Title = updatedEvent.Title;
			existingEvent.ImageUrls = updatedEvent.ImageUrls;
			existingEvent.Published = updatedEvent.Published;
			existingEvent.ThumbnailUrl = updatedEvent.ThumbnailUrl;
			existingEvent.Urls = updatedEvent.Urls;
			existingEvent.LastUpdated = DateTime.Now;

			var updateOperation = TableOperation.Replace(existingEvent);

			// Execute the operation.
			var updateResults = await eventsTable.ExecuteAsync(updateOperation);

			if (updateResults.HttpStatusCode == (int)HttpStatusCode.NoContent)
			{
				return new OkObjectResult("Event updated");
			}
			else
			{
				return new BadRequestObjectResult("Failed to update event");
			}
		}

		private static async Task<IActionResult> HandleEventPostRequest(HttpRequest req, CloudTable eventsTable, ILogger log)
		{
			var jsonString = await req.ReadAsStringAsync();
			var newEvent = JsonConvert.DeserializeObject<Event>(jsonString);

			// Create the TableOperation object that inserts the customer entity.
			var insertOperation = TableOperation.Insert(newEvent);

			// Execute the insert operation.
			var results = await eventsTable.ExecuteAsync(insertOperation);

			if (results.HttpStatusCode == (int)HttpStatusCode.NoContent)
			{
				return new OkObjectResult("Event added");
			}
			else
			{
				return new BadRequestObjectResult("Failed to add event");
			}
		}

		private static async Task<IActionResult> HandleEventGetRequest(CloudTable eventsTable, ILogger log)
		{
			log.LogInformation("Events processed a GET request.");
			var query = new TableQuery<Event>();
			var events = await eventsTable.ExecuteQuerySegmentedAsync(query, null);

			return (ActionResult)new OkObjectResult(events.ToList());
		}
	}
}

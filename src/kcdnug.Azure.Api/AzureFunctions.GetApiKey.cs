using System;
using System.Threading.Tasks;
using kcdnug.Azure.Api.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace kcdnug.Azure.Api
{
	public static partial class AzureFunctions
    {
        [FunctionName("GetApiKey")]
        public static async Task<IActionResult> RunGetApiKey([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("GetApiKey processed a request.");

            string credentials = req.Query["MeetupCredentials"];

	        var isValid = await MeetupConnector.ValidateCredentials(credentials);
	        var apiKey = Environment.GetEnvironmentVariable("ApiKey");

			return isValid
                ? (ActionResult)new OkObjectResult(apiKey)
                : new BadRequestObjectResult("Please pass credentials to the function");
        }
	}
}

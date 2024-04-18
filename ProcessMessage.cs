using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;
using Newtonsoft.Json;

namespace Hello.Dotnet.FunctionApp
{
    public class ProcessMessage
    {
        readonly ILogger<ProcessMessage> _logger;

        public ProcessMessage(ILogger<ProcessMessage> logger)
        {
            _logger = logger;
        }

        [Function("GetMessage")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "message")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            _logger.LogWarning("This is a warning message");
            _logger.LogError("This is an error message");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Hello.Dotnet.FunctionApp
{
    public static class GetClientIp
    {
        [FunctionName("GetClientIp")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var clientIp = req.HttpContext.Connection.RemoteIpAddress.ToString();

            return new OkObjectResult(clientIp);
        }
    }
}

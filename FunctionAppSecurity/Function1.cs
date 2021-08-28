using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FunctionAppSecurity
{
    public static class Function1
    {

        //The AuthorizationLevel.Function can be set on the Azure Function to require an API Key. 
        //    By setting the enum to Function, you ensure that a deployed instance of the functions will required at least a Function Key
        //    to access the resource behind the API.A Host API Key will also grant access to this level of authorization.

        //Check the readme file
        /// <summary>
        ///  Anonymous -- no key needed
        ///  Function -- Function or host keys
        ///  Admin -- Host Keys (master)
        ///  System --  Host key (master)
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("KeyAuth")]
        public static async Task<IActionResult> Run(
             [HttpTrigger(AuthorizationLevel.System, "get", Route = null)] HttpRequest req,
             ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}

using FunctionIdentityUserAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FunctionAppSecurity
{
    public class ADSecuredFunction
    {
        public ADSecuredFunction(AzureADJwtBearerValidation azureADJwtBearerValidation)
        {
            AzureADJwtBearerValidation = azureADJwtBearerValidation;
        }

        public AzureADJwtBearerValidation AzureADJwtBearerValidation { get; }

        [FunctionName("ADSecuredFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            ClaimsPrincipal principal; // This can be used for any claims
            if ((principal = await AzureADJwtBearerValidation.ValidateTokenAsync(req.Headers["Authorization"])) == null)
            {
                return new UnauthorizedResult();
            }

            return new OkObjectResult($"Bearer token claim preferred_username: {AzureADJwtBearerValidation.GetPreferredUserName()}");

        }
    }
}

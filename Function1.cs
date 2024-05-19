using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Text;

namespace PowerBiApi
{
    public class Function1
    {
        [Function("PushDataToPowerBi")]
        public static async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req)
        {
            

            // Get the JSON payload from the request body
            string payload = await req.ReadAsStringAsync();

            // Replace with your Power BI Push URI
            string pushUri = "https://api.powerbi.com/beta/e6083e94-48b0-4cad-b9d2-3ead7a92eeb2/datasets/75b20ad3-5521-4cde-8248-b4dce860633b/rows?experience=power-bi&key=O7BVjBLDdw9E88JXeY8%2BWuRl21YBodPyTAw6Pk6RcDRFJeXEaLw%2BGQqwhnaSHmisQpnXm2RgynUzK6QL3SjX4w%3D%3D";

            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, pushUri)
                {
                    Content = new StringContent(payload, Encoding.UTF8, "application/json")
                };

                var response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    
                    return new OkResult();
                }
                else
                {
                    return new BadRequestResult();
                }
            }
        }
    }
}

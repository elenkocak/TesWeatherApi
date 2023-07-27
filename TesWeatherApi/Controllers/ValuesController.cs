using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace TesWeatherApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public ActionResult Index()
        {
            // Create an AccuWeather API account and get an API key.
            string apiKey = "h8cqWTwlzAn5PuLGKZTOGsgDdnDnuzBt";

            // Create a `HttpClient` object to make the request.
            HttpClient client = new HttpClient();

            // Create an `HttpRequestMessage` object to specify the request.
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://api.accuweather.com/v1/weather/alerts/5day/locationkey/YOUR_LOCATION_KEY");

            // Set the `Authorization` header with the API key.
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            // Send the request.
            HttpResponseMessage response = client.Send(request);

            // Process the response.
            if (response.IsSuccessStatusCode)
            {
                // The request was successful.
                string responseBody = response.Content.ReadAsStringAsync().Result;

                // Deserialize the response body into a collection of `WeatherAlert` objects.
                var alerts = JsonConvert.DeserializeObject<IEnumerable<WeatherAlert>>(responseBody);

                // Return the collection of `WeatherAlert` objects to the client.
                return Json(alerts);
            }
            else
            {
                // The request failed.
                HandleError(response);
            }
        }

        private void HandleError(HttpResponseMessage response)
        {
            // TODO: Handle the error.
        }
    }

    public class WeatherAlert
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string LocationKey { get; set; }
        public string Severity { get; set; }
        public string EventType { get; set; }
    }
}
    

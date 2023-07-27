using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        [HttpGet("")]
        public ActionResult Index()
        {
            // Uygulamaya login oldum, MyApps kısmından yeni proje oluşturup key aldım.
            string apiKey = "JYyp2F21rBMGok5Dy6orCF07jbRJRsLB";

            // İstek atmak için client nesnesi oluşturdum
            HttpClient client = new HttpClient();

            // Bizim önce location key'e ihtiyacımız var
            //HttpRequestMessage requestLocation = new HttpRequestMessage(HttpMethod.Get, "http://api.accuweather.com/locations/v1/search?q=san&apikey={JYyp2F21rBMGok5Dy6orCF07jbRJRsLB}");
            HttpRequestMessage request2 = new HttpRequestMessage(HttpMethod.Get, "http://api.accuweather.com/locations/v1/search?q=san&apikey={JYyp2F21rBMGok5Dy6orCF07jbRJRsLB}");

            request2.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            HttpResponseMessage response = client.Send(request2);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;

                var alerts = JsonConvert.DeserializeObject<IEnumerable<WeatherAlert>>(responseBody);

                return Ok(alerts);
            }
            else
            {
                // The request failed.
                HandleError(response);
            }
            return BadRequest();
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

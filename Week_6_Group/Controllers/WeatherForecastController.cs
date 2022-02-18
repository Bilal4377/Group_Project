using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using Newtonsoft.Json;

namespace Week_6_Group.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet(Name = "GetWeatherForecast")]
        public ActionResult<List<Root>> Get(string city)
        {
            HttpClient client = new HttpClient();
            dynamic? obj = new ExpandoObject();
            string result;

            try
            {
                HttpResponseMessage response = client.GetAsync("https://weatherdbi.herokuapp.com/data/weather/" + city).Result;
                response.EnsureSuccessStatusCode();
                result = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            Root? list = JsonConvert.DeserializeObject<Root>(result);
            return Ok(list);
        }
        public class Root
        {
            public string? region { get; set; }
            public CurrentConditions? currentConditions { get; set; }
            public List<NextDay>? next_days { get; set; }
        }
        public class CurrentConditions
        {
            public string? dayhour { get; set; }
            public Temp? temp { get; set; }
            public string? precip { get; set; }
            public string? humidity { get; set; }
            public Wind? wind { get; set; }
            public string? comment { get; set; }
        }
        public class NextDay
        {
            public string? day { get; set; }
            public string? comment { get; set; }
            public MaxTemp? max_temp { get; set; }
            public MinTemp? min_temp { get; set; }
        }
        public class Temp
        {
            public int? c { get; set; }
            public in?t f { get; set; }
        }
        public class Wind
        {
            public int? km { get; set; }
            public int? mile { get; set; }
        }
        public class MaxTemp
        {
            public int? c { get; set; }
            public int? f { get; set; }
        }
        public class MinTemp
        {
            public int? c { get; set; }
            public int? f { get; set; }
        }
    }
}

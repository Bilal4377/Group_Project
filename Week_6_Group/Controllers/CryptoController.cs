using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Week_6_Group.Controllers
{
    [Route("api/GetRecentMaximumValueTrade")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        [HttpGet(Name = "GetRecentMaximumValueTrade")]
        public ActionResult Get(string symbol)
        {
            HttpClient client = new HttpClient();
            dynamic? obj = new ExpandoObject();
            string result;

            try
            {
                HttpResponseMessage response = client
                    .GetAsync("https://api.binance.com/api/v3/trades?symbol=" + symbol)
                    .Result;
                response.EnsureSuccessStatusCode();
                result = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            List<Trade>? trades = JsonConvert.DeserializeObject<List<Trade>>(result);
            decimal maxValue = 0;
            Trade maxValueTrade = trades[0];
            foreach (Trade trade in trades)
            {
                decimal value = (decimal)trade.price * (decimal)trade.qty;
                if (value > maxValue)
                {
                    maxValue = value;
                    maxValueTrade = trade;
                }
            }
            return Ok(maxValueTrade);
        }
    }
    public class Trade
    {
        public Int64 id { get; set; }
        public decimal? price { get; set; }
        public decimal? qty { get; set; }
        public decimal? quoteQty { get; set; }
        public Int64? time { get; set; }
        public bool? isBuyerMaker { get; set; }
        public bool? isBestMatch { get; set; }
    }
}

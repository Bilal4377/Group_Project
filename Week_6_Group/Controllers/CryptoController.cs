using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Json;

namespace Week_6_Group.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        [Route("1/sumOfPrices")]
        [HttpGet]
        public ActionResult<Trade> Get(string symbol)
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
            var lDouble = new List<decimal>();

            foreach (Trade trade in trades)
            {
                lDouble.Add(trade.price);
            }

            decimal sumOfPrices = lDouble.Sum();

            //ExchangeInformation? list = JsonConvert.DeserializeObject<ExchangeInformation>(result);
            return Ok(sumOfPrices);
        }

        [Route("2/maxValue")]
        [HttpGet]
        public ActionResult<Trade> Get2(string symbol)
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

            //ExchangeInformation? list = JsonConvert.DeserializeObject<ExchangeInformation>(result);
            return Ok(maxValueTrade);
        }

        [Route("3/avgfPrices")]
        [HttpGet]
        public ActionResult<Trade> Get3(string symbol)
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
            var lDopuble = new List<decimal>();

            foreach (Trade trade in trades)
            {
                lDopuble.Add(trade.price);
            }

            decimal avgfPrices = lDopuble.Average();

            //ExchangeInformation? list = JsonConvert.DeserializeObject<ExchangeInformation>(result);
            return Ok(avgfPrices);
        }

        [Route("4/numOfTrades")]
        [HttpGet]
        public ActionResult<Trade> Get4(string symbol)
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
            var lDopuble = new List<decimal>();

            foreach (Trade trade in trades)
            {
                lDopuble.Add(trade.id);
            }

            decimal numOfTrades = lDopuble.Count();

            //ExchangeInformation? list = JsonConvert.DeserializeObject<ExchangeInformation>(result);
            return Ok(numOfTrades);
        }

        public class Trade
        {
            public Int64 id { get; set; }
            public decimal price { get; set; }
            public decimal? qty { get; set; }
            public decimal? quoteQty { get; set; }
            public Int64? time { get; set; }
            public bool? isBuyerMaker { get; set; }
            public bool? isBestMatch { get; set; }
        }
    }
} 

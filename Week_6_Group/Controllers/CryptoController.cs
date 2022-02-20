using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Week_6_Group.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            HttpClient client = new HttpClient();
            dynamic? obj = new ExpandoObject();
            string result;

            try
            {
                HttpResponseMessage response = client
                    .GetAsync("https://api.binance.com/api/v3/trades?symbol=" + "BNBBTC")
                    .Result;
                response.EnsureSuccessStatusCode();
                result = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            List<Trade>? trades = JsonConvert.DeserializeObject<List<Trade>>(result);

            
            //ExchangeInformation? list = JsonConvert.DeserializeObject<ExchangeInformation>(result);
            return Ok(trades);
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
        //public decimal value;

        //public void setValue()
        //{
        //    decimal value = price * qty;
        //}
    }
    //THE FOLLOWING IS THE COMMENTED OUT MODELS WE USED WHEN TINKERING WITH THE EXCHANGEINFORMATION ROUTE
    //WE ABANDONED THIS IN ORDER TO PERFOM CALCULATIONS USING THE TRADE ROUTE INSTEAD
    //
    //public class ExchangeInformation
    //{
    //    [System.Diagnostics.CodeAnalysis.
    //        SuppressMessage("Style", "IDE1006:Naming Styles", 
    //        Justification = "<deserialization is case sensitive so adhering to the standard would break the API>")]
    //    public string? timezone { get; set; }
    //    //ignore IDE1006 for all variables because deserialization is case sensitive,
    //    //so adhering to the standard would break the API
    //    public Int64 serverTime { get; set; }
    //    public List<RateLimit>? rateLimits { get; set; } 
    //    public List<string>? exchangeFilters { get; set; }
    //    public List<Symbols>? symbols { get; set; }

    //}
    
    //public class RateLimit
    //{
    //    public string? rateLimitType { get; set; }
    //    public string? interval { get; set; }
    //    public int intervalNum { get; set; }
    //    public int limit{ get; set; }
    //}
    //public class Symbols
    //{
    //    public string? symbol { get; set; }
    //    public string? status { get; set; }
    //    public string? baseAsset { get; set; }
    //    public int? baseAssetPrecision { get; set; }
    //    public string? quoteAsset { get; set; }
    //    public int? quotePrecision { get; set; }
    //    public int? quoteAssetPrecision { get; set; }
    //    public int? baseCommissionPrecision { get; set; }
    //    public int? quoteCommissionPrecision { get; set; }
    //    public List<string>? orderTypes { get; set; }
    //    public bool? icebergAllowed { get; set; }
    //    public bool? ocoAllowed { get; set; }
    //    public bool? quoteOrderQtyMarketAllowed { get; set; }
    //    public bool? isSpotTradingAllowed { get; set; }
    //    public bool? isMarginTradingAllowed { get; set; }
    //    public List<string>? permissions { get; set; }
    //}
}

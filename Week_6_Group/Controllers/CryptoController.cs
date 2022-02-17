using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Week_6_Group.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get(string symbol)
        {
            return Ok();
        }
    }

    public class root
    {
        public string timezone { get; set; }
        public int serverTime { get; set; }
        //public RateLimits rateLimits { get; set; }


    }
}

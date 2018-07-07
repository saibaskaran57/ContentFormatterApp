namespace ContentFormatterApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ContentFormatterApp.Models;

    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Request request)
        {
            return Ok(new Response { Message = "OK" });
        }
    }
}

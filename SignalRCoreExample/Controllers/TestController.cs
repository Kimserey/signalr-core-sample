using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SignalRCoreExample.Controllers
{
    [Authorize]
    [Route("Test")]
    public class TestController: Controller
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Hello world";
        }
    }
}

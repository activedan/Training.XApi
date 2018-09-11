using Microsoft.AspNetCore.Mvc;

namespace Training.XApi.Controllers
{
    [Route("ping")]
    public class PingController : Controller
    {
        public PingController()
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}

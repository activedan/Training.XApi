using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Yokozuna.Logging.Extensions;

namespace Training.XApi.Controllers
{
    [Route("ping")]
    public class PingController : Controller
    {
        private ILogger<PingController> _logger;

        public PingController(ILogger<PingController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpGet]
        [Route("log")]
        public IActionResult Log(string message)
        {
            _logger.Error(message);

            return Ok();
        }
    }
}

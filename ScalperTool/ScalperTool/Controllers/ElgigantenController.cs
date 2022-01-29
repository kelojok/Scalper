using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScalperTool.Scalper.Interfaces;

namespace ScalperTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElgigantenController : ControllerBase
    {
        private readonly IPlaystationFiveScalper _playstationFiveScalper;
        public ElgigantenController(IPlaystationFiveScalper playstationFiveScalper)
        {
            _playstationFiveScalper = playstationFiveScalper;
        }

        [HttpGet("playstationfive/stock")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public ActionResult<bool> AcceptsPlaystationFiveOrders()
        {
            var result = _playstationFiveScalper.AcceptOrdersElgiganten();

            return Ok(result);
        }
    }
}

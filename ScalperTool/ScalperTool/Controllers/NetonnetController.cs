using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScalperTool.Scalper.Interfaces;

namespace ScalperTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    public class NetonnetController : ControllerBase
    {
        private readonly IPlaystationFiveScalper _playstationFiveScalper;
        public NetonnetController(IPlaystationFiveScalper playstationFiveScalper)
        {
            _playstationFiveScalper = playstationFiveScalper;
        }

        [HttpGet("playstationfive/stock")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public ActionResult<bool> PlaystationFiveIsInStock()
        {
            var result = _playstationFiveScalper.InStockOrBookableNetonnet();

            return Ok(result);
        }
    }
}

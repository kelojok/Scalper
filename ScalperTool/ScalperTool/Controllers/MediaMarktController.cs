using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScalperTool.Scalper.Interfaces;

namespace ScalperTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaMarktController : ControllerBase
    {
        private readonly IPlaystationFiveScalper _playstationFiveScalper;
        public MediaMarktController(IPlaystationFiveScalper playstationFiveScalper)
        {
            _playstationFiveScalper = playstationFiveScalper;
        }

        [HttpGet("playstationfive/stock")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public ActionResult<bool> PlaystationFiveIsInStock()
        {
            var result = _playstationFiveScalper.InStockOrBookableMediaMarkt();

            return Ok(result);
        }
    }
}

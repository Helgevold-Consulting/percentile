using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    public class PercentileController : ControllerBase
    {
        private IPercentileService _percentileService;
        private IPercentileDataAccess _percentileDataAccess;

        public PercentileController(IPercentileService percentileService, IPercentileDataAccess percentileDataAccess)
        {
            _percentileService = percentileService;
            _percentileDataAccess = percentileDataAccess;
        }

        // GET api/percentile
        [HttpGet]
        [Route("api/[controller]/{percentile}")]
        public ActionResult<double> Get(double percentile)
        {
            try
            {
                var data = this._percentileDataAccess.GetPercentileData();
                return _percentileService.CalculatePercentile(percentile, data);
            }
            catch(ArgumentOutOfRangeException)
            {
                //TODO log error
                return StatusCode(400, "Bad Request");
            }
            catch (Exception)
            {
                //TODO log error
                return StatusCode(503, "Service Unavailable");
            }

        }
    }
}

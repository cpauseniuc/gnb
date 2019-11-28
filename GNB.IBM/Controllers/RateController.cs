using AutoMapper;
using GNB.IBM.Api.ViewModels;
using GNB.IBM.HerokuApp.Proxy;
using GNB.IBM.HerokuApp.Proxy.Entities;
using GNB.IBM.HerokuApp.Proxy.Infraestructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GNB.IBM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : BaseController
    {
        public RateController(DataManager dataManager, IMapper mapper) : base(dataManager,  mapper) { }

        [HttpGet("getRates")]
        public async Task<IActionResult> GetRates()
        {
            try
            {
                var rateRecords = await _dataManager.GetRates();
                if (rateRecords.Any())
                    return new OkObjectResult(_autoMapper.Map<List<RateViewModel>>(rateRecords));
                else
                    return BadRequest(new ErrorHandling.ApiError("No data avaible. This may be caused by a first call with host down.",null));
            }
            catch (ProxyException pex)
            {
                return BadRequest(new ErrorHandling.ApiError(pex.Message, pex));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorHandling.ApiError(ex.Message, ex));
            }
        }
    }
}

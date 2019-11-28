using AutoMapper;
using GNB.IBM.HerokuApp.Proxy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GNB.IBM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public readonly DataManager _dataManager;
        public readonly IMapper _autoMapper;
        public BaseController(DataManager dataManager, IMapper mapper)
        {
            this._dataManager = dataManager;
            this._autoMapper = mapper;
        }
    }
}

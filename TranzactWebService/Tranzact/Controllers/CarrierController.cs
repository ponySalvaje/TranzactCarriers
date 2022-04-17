using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using Tranzact.Models.Request;
using Tranzact.Models.Response;
using Tranzact.Providers;
using Tranzact.AppServices;
using Microsoft.AspNetCore.Cors;

namespace Tranzact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowOrigin")]
    public class CarrierController : ControllerBase
    {
        private readonly ILogger<CarrierController> _logger;
        private readonly CarrierProvider _carrierProvider;

        public CarrierController(IOptions<MyAppSettings> _options, ILogger<CarrierController> logger)
        {
            _logger = logger;
            _carrierProvider = new CarrierProvider(_options);
        }

        [HttpGet]
        public IEnumerable<CarrierResponse> Get([FromQuery] CarrierRequest carrierRequest) => _carrierProvider.GetCarriers(carrierRequest);
    }
}

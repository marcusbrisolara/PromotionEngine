using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Promotion.UI.Entities;
using Promotion.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Promotion.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromotionsController : ControllerBase
    {
        private readonly ILogger<PromotionsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IEngineService _engineService;

        public PromotionsController(ILogger<PromotionsController> logger, IEngineService engineService, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _engineService = engineService;
        }

        [HttpPost]
        public Cart Post([FromBody]Cart cart)
        {
            return _engineService.ProcessPromotions(cart);
        }
    }
}

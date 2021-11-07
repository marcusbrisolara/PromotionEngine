using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        public PromotionsController(ILogger<PromotionsController> logger)
        {
            _logger = logger;
        }
    }
}

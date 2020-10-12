using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PromotionEngine.Core;
using PromotionEngine.Core.Models;

namespace PromotionEngine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionEngine _promotionEngine;
        private readonly ILogger<PromotionController> _logger;
        public PromotionController(IPromotionEngine promotionEngine, ILogger<PromotionController> logger)
        {
            _promotionEngine = promotionEngine;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<double>> ApplyPromoAndCalculateBill(Cart cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _promotionEngine.ApplyPromo(cart);
            return cart.DiscountedTotal;
        }
    }
}

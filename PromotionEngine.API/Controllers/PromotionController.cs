using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly ILogger<PromotionController> _logger;
        public PromotionController(IPromotionEngine promotionEngine, IMapper mapper, ILogger<PromotionController> logger)
        {
            _promotionEngine = promotionEngine;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<double>> ApplyPromoAndCalculateBill(DTO.Cart cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var  updateCart = _promotionEngine.ApplyPromo(_mapper.Map<Cart>(cart));
            return updateCart.DiscountedTotal;
        }
    }
}

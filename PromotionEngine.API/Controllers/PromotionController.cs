using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PromotionEngine.Core;
using PromotionEngine.Core.Models;
using PromotionEngine.Core.Promotions;
using PromotionEngine.Core.PromotionService;

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
        [Route("[action]")]
        public async Task<ActionResult<double>> Apply(DTO.Cart cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var  updateCart = _promotionEngine.ApplyPromo(_mapper.Map<Cart>(cart));
            return Ok(updateCart.DiscountedTotal);
        }
        [HttpPost]
        public async Task<ActionResult<Promotion>> Post(Promotion promotion)
        {
            _promotionEngine.Add(promotion);
            return Ok(promotion);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Promotion promotion)
        {
            _promotionEngine.Remove(promotion);
            return Ok();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PromotionEngine.Core.Models;
using PromotionEngine.Core.ProductService;

namespace PromotionEngine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _product;
        public ProductController(ILogger<ProductController> logger, IProductService product)
        {
            _product = product;
            _logger = logger;
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult> Get(string productId)
        {
            return Ok(_product.Get(productId));
        }
        [HttpPost]
        public async Task<ActionResult> Post(IEnumerable<Product> products)
        {
            _product.Add(products);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Put(IEnumerable<Product> products)
        {
            _product.Update(products);
            return Ok();
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(IEnumerable<Product> products)
        {
            _product.Remove(products);
            return Ok();
        }
    }
}

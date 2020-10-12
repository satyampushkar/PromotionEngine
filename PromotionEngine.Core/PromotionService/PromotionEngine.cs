using Microsoft.Extensions.Logging;
using PromotionEngine.Core.Models;
using PromotionEngine.Core.ProductService;
using PromotionEngine.Core.Promotions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionEngine.Core.PromotionService
{   
    public class PromotionEngine : IPromotionEngine
    {
        private readonly SortedSet<Promotion> _promotions;
        private readonly IProductService _products;
        private readonly ILogger<PromotionEngine> _logger;
        public PromotionEngine(ILogger<PromotionEngine> logger, IProductService products)
        {
            _products = products;
            _promotions = new SortedSet<Promotion>(new ComparerForPromotionByPriority())
            {
                new BuyNForFixedPrice
                {
                    Priority = 1,
                    Product = _products.Get("A"),
                    NValue = 3,
                    FixedPrice = 130
                },
                new BuyNForFixedPrice
                {
                    Priority = 2,
                    Product = _products.Get("B"),
                    NValue = 2,
                    FixedPrice = 45
                },
                new BuyXAndYForFixedPrice
                {
                    Priority = 3,
                    ProductX = _products.Get("C"),
                    ProductXReqdQty = 1,
                    ProductY = _products.Get("D"),
                    ProductYReqdQty = 1,
                    FixedPrice = 30
                }
            };
            _logger = logger;
        }
        public void Add(Promotion promotion)
        {
            try
            {
                _promotions.Add(promotion);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception at PromotionEngine.Add: {ex.Message}");
            }
        }
        public void Remove(Promotion promotion)
        {
            try
            {
                _promotions.Remove(promotion);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception at PromotionEngine.Remove: {ex.Message}");
            }
        }
        public Cart ApplyPromo(Cart cart)
        {
            try
            {
                foreach (var cartItem in cart.CartItem)
                {
                    cartItem.Product.Price = _products.Get(cartItem.Product.SKUId).Price;
                }
                foreach (var promotion in _promotions)
                {
                    promotion.Apply(cart);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception at PromotionEngine.ApplyPromo: {ex.Message}");
            }
            return cart;
        }
    }
}

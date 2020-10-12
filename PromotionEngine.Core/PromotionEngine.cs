using Microsoft.Extensions.Logging;
using PromotionEngine.Core.Models;
using PromotionEngine.Core.Promotions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Core
{   
    public class PromotionEngine : IPromotionEngine
    {
        private readonly SortedSet<Promotion> _promotions;
        private readonly ILogger<PromotionEngine> _logger;
        public PromotionEngine(ILogger<PromotionEngine> logger)
        {
            _promotions = new SortedSet<Promotion>(new ComparerForPromotionByPriority());
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

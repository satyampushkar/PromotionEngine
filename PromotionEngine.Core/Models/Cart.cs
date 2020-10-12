using System.Collections.Generic;

namespace PromotionEngine.Core.Models
{
    public class Cart
    {
        public IEnumerable<CartItem> CartItem { get; set; }
        public double Total { get; set; }
        public double DiscountedTotal { get; set; }
    }
}

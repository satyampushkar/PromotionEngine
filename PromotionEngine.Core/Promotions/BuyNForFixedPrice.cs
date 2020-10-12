using PromotionEngine.Core.Models;
using System.Linq;

namespace PromotionEngine.Core.Promotions
{
    public class BuyNForFixedPrice : Promotion
    {
        public Product Product { get; set; }
        public int NValue { get; set; }
        public double FixedPrice { get; set; }

        public override void Apply(Cart cart)
        {
            var CartItem = cart.CartItem.FirstOrDefault(item => item.Product.SKUId == Product.SKUId);
            if (CartItem != null)
            {
                CartItem.DiscountedTotal =
                    ((CartItem.Quantity / NValue) * FixedPrice) +
                    ((CartItem.Quantity % NValue) * CartItem.Product.Price);
                cart.DiscountedTotal += CartItem.DiscountedTotal;
            }
        }
    }
}

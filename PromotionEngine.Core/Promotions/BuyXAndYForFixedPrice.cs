using PromotionEngine.Core.Models;
using System.Linq;

namespace PromotionEngine.Core.Promotions
{
    public class BuyXAndYForFixedPrice : Promotion
    {
        public Product ProductX { get; set; }
        public Product ProductY { get; set; }
        public int ProductXReqdQty { get; set; }
        public int ProductYReqdQty { get; set; }
        public double FixedPrice { get; set; }
        public override void Apply(Cart cart)
        {
            var CartItemX = cart.CartItem.FirstOrDefault(item => item.Product.SKUId == ProductX.SKUId);
            var CartItemY = cart.CartItem.FirstOrDefault(item => item.Product.SKUId == ProductY.SKUId);
            if (CartItemX != null && CartItemY != null)
            {
                if (CartItemX.Quantity >= ProductXReqdQty && CartItemY.Quantity >= ProductYReqdQty)
                {
                    int xGroup = CartItemX.Quantity / ProductXReqdQty;
                    int yGroup = CartItemY.Quantity / ProductYReqdQty;
                    int commonGroup;
                    if (xGroup > yGroup)
                    {
                        commonGroup = xGroup - yGroup;
                    }
                    else if (xGroup < yGroup)
                    {
                        commonGroup = yGroup - xGroup;
                    }
                    else
                    {
                        commonGroup = xGroup;
                    }

                    CartItemX.DiscountedTotal = (CartItemX.Quantity - (commonGroup * ProductXReqdQty)) * CartItemX.Product.Price;
                    CartItemY.DiscountedTotal = (commonGroup * FixedPrice) +
                        (CartItemY.Quantity - (commonGroup * ProductYReqdQty)) * CartItemY.Product.Price;
                }
                else
                {
                    CartItemX.DiscountedTotal = CartItemX.Quantity * CartItemX.Product.Price;
                    CartItemY.DiscountedTotal = CartItemY.Quantity * CartItemY.Product.Price;
                }
                cart.DiscountedTotal += (CartItemX.DiscountedTotal + CartItemY.DiscountedTotal);
            }
            else
            {
                if (CartItemX != null)
                {
                    CartItemX.DiscountedTotal = CartItemX.Quantity * CartItemX.Product.Price;
                    cart.DiscountedTotal += CartItemX.DiscountedTotal;
                }
                if (CartItemY != null)
                {
                    CartItemY.DiscountedTotal = CartItemY.Quantity * CartItemY.Product.Price;
                    cart.DiscountedTotal += CartItemY.DiscountedTotal;
                }
            }
        }
    }
}

namespace PromotionEngine.Core.Models
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
        public double DiscountedTotal { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngine.API.DTO
{
    public class Cart
    {
        public IEnumerable<CartItem> CartItems { get; set; }
    }
}

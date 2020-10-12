using PromotionEngine.Core.Models;
using PromotionEngine.Core.Promotions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Core
{
    public interface IPromotionEngine
    {
        void Add(Promotion promotion);
        Cart ApplyPromo(Cart cart);
        void Remove(Promotion promotion);
    }
}

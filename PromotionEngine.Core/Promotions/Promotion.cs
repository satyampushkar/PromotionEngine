using PromotionEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace PromotionEngine.Core.Promotions
{
    public abstract class Promotion
    {
        public int Priority { get; set; }
        //public List<Product> ApplicableProducts { get; set; }
        public abstract void Apply(Cart cart);
    }
    public class ComparerForPromotionByPriority : IComparer<Promotion>
    {
        public int Compare([AllowNull] Promotion x, [AllowNull] Promotion y)
        {
            return Math.Sign(x.Priority - y.Priority);
        }
    }
}

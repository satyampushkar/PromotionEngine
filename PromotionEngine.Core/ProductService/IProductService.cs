using PromotionEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Core.ProductService
{
    public interface IProductService
    {
        void Add(IEnumerable<Product> products);
        void Remove(IEnumerable<Product> products);
        void Update(IEnumerable<Product> products);
        Product Get(string productId);
    }
}

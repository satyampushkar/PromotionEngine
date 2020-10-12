using PromotionEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionEngine.Core.ProductService
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products;
        public ProductService()
        {
            _products = new List<Product>()
            {
                new Product { SKUId = "A", Price = 50 },
                new Product { SKUId = "B", Price = 30 },
                new Product { SKUId = "C", Price = 20 },
                new Product { SKUId = "D", Price = 15 }
            };
        }
        public void Add(IEnumerable<Product> products)
        {
            throw new NotImplementedException();
        }

        public Product Get(string productId)
        {
            return _products.FirstOrDefault(product => product.SKUId == productId);
        }

        public void Remove(IEnumerable<Product> products)
        {
            throw new NotImplementedException();
        }
        public void Update(IEnumerable<Product> products)
        {
            throw new NotImplementedException();
        }
    }
}

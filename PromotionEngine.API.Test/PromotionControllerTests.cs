using Microsoft.AspNetCore.Mvc.Testing;
using PromotionEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace PromotionEngine.API.Test
{
    [Collection("PromotionController Tests")]
    public class PromotionControllerTests
    {
        private readonly WebApplicationFactory<Startup> _factory;
        public PromotionControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [MemberData(nameof(CartData))]
        public async Task TestBillValue(Cart cart, double expectedBillValue)
        {
            // Arrange
            double actualBillValue;
            var client = _factory.CreateClient();

            if (cart == null)
            {
                actualBillValue = -1;
            }
            else
            {
                string strPayLoad = JsonSerializer.Serialize<Cart>(cart, new JsonSerializerOptions());
                var response = await client.PostAsync("api/promotion", new StringContent(strPayLoad, Encoding.UTF8, "application/json"));

                // Assert
                response.EnsureSuccessStatusCode();
                Assert.NotNull(response.Content);
                
                string res = response.Content.ReadAsStringAsync().Result;
                actualBillValue = Convert.ToDouble(res);
            }

            Assert.Equal(expectedBillValue, actualBillValue);
        }
        public static IEnumerable<object[]> CartData()
        {
            Product productA = new Product { SKUId = "A", Price = 50 };
            Product productB = new Product { SKUId = "B", Price = 30 };
            Product productC = new Product { SKUId = "C", Price = 20 };
            Product productD = new Product { SKUId = "D", Price = 15 };

            //Scenario A
            Cart cart1 = new Cart();
            var cartItemList = new List<CartItem>();
            cartItemList.Add(new CartItem { Product = productA, Quantity = 1 });
            cartItemList.Add(new CartItem { Product = productB, Quantity = 1 });
            cartItemList.Add(new CartItem { Product = productC, Quantity = 1 });
            cart1.CartItem = cartItemList;

            //Scenario B
            Cart cart2 = new Cart();
            cartItemList = new List<CartItem>();
            cartItemList.Add(new CartItem { Product = productA, Quantity = 5 });
            cartItemList.Add(new CartItem { Product = productB, Quantity = 5 });
            cartItemList.Add(new CartItem { Product = productC, Quantity = 1 });
            cart2.CartItem = cartItemList;

            //Scenario C
            Cart cart3 = new Cart();
            cartItemList = new List<CartItem>();
            cartItemList.Add(new CartItem { Product = productA, Quantity = 3 });
            cartItemList.Add(new CartItem { Product = productB, Quantity = 5 });
            cartItemList.Add(new CartItem { Product = productC, Quantity = 1 });
            cartItemList.Add(new CartItem { Product = productD, Quantity = 1 });
            cart3.CartItem = cartItemList;

            return new List<object[]>
            {
                new object[] { cart1, 100 },
                new object[] { cart2, 370 },
                new object[] { cart3, 280 },
            };
        }
    }
}

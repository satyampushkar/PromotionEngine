using Microsoft.AspNetCore.Mvc.Testing;
using PromotionEngine.API.DTO;
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
        public async Task TestBillValue(DTO.Cart cart, double expectedBillValue)
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
                string strPayLoad = JsonSerializer.Serialize<DTO.Cart>(cart, new JsonSerializerOptions());
                var response = await client.PostAsync("api/promotion/apply", new StringContent(strPayLoad, Encoding.UTF8, "application/json"));

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
            //Scenario A
            DTO.Cart cart1 = new DTO.Cart();
            cart1.CartItems = new List<DTO.CartItem>() 
            {
                new DTO.CartItem{ ProductId ="A", Quantity = 1},
                new DTO.CartItem{ ProductId ="B", Quantity = 1},
                new DTO.CartItem{ ProductId ="C", Quantity = 1},
            };

            //Scenario B
            DTO.Cart cart2 = new DTO.Cart();
            cart2.CartItems = new List<DTO.CartItem>()
            {
                new DTO.CartItem{ ProductId ="A", Quantity = 5},
                new DTO.CartItem{ ProductId ="B", Quantity = 5},
                new DTO.CartItem{ ProductId ="C", Quantity = 1},
            };

            //Scenario C
            DTO.Cart cart3 = new DTO.Cart();
            cart3.CartItems = new List<DTO.CartItem>()
            {
                new DTO.CartItem{ ProductId ="A", Quantity = 3},
                new DTO.CartItem{ ProductId ="B", Quantity = 5},
                new DTO.CartItem{ ProductId ="C", Quantity = 1},
                new DTO.CartItem{ ProductId ="D", Quantity = 1},
            };
            return new List<object[]>
            {
                new object[] { cart1, 100 },
                new object[] { cart2, 370 },
                new object[] { cart3, 280 },
            };
        }
    }
}

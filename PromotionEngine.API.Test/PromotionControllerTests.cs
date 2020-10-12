using Microsoft.AspNetCore.Mvc.Testing;
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
        [InlineData("", 0)]
        public async Task TestBillValue(string cart, double expectedBillValue)
        {
            // Arrange
            double actualBillValue;
            var client = _factory.CreateClient();

            if (string.IsNullOrWhiteSpace(cart))
            {
                actualBillValue = -1;
            }
            else
            {
                string strPayLoad = JsonSerializer.Serialize(cart, new JsonSerializerOptions());
                var response = await client.PostAsync("api/promotion", new StringContent(strPayLoad, Encoding.UTF8, "application/json"));

                // Assert
                response.EnsureSuccessStatusCode();
                Assert.NotNull(response.Content);
                
                string res = response.Content.ReadAsStringAsync().Result;
                actualBillValue = Convert.ToDouble(res);
            }

            Assert.Equal(expectedBillValue, actualBillValue);
        }
    }
}

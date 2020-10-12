using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PromotionEngine.API.Test
{
    [CollectionDefinition("PromotionController Tests")]
    public class TestCollection : ICollectionFixture<WebApplicationFactory<PromotionEngine.API.Startup>>
    {
    }
}

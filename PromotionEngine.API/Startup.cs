using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PromotionEngine.Core;
using PromotionEngine.Core.Models;
using PromotionEngine.Core.Promotions;

namespace PromotionEngine.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<IPromotionEngine, Core.PromotionEngine>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            InitializePromotionEngine(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void InitializePromotionEngine(IApplicationBuilder app)
        {
            //
            Product productA = new Product { SKUId = "A", Price = 50 };
            Product productB = new Product { SKUId = "B", Price = 30 };
            Product productC = new Product { SKUId = "C", Price = 20 };
            Product productD = new Product { SKUId = "D", Price = 15 };
            var promoEngine = app.ApplicationServices.GetService<IPromotionEngine>();
            promoEngine.Add(new BuyNForFixedPrice
            {
                Priority = 1,
                Product = productA,
                NValue = 3,
                FixedPrice = 130
            });

            promoEngine.Add(new BuyNForFixedPrice
            {
                Priority = 2,
                Product = productB,
                NValue = 2,
                FixedPrice = 45
            });

            promoEngine.Add(new BuyXAndYForFixedPrice
            {
                Priority = 3,
                ProductX = productC,
                ProductXReqdQty = 1,
                ProductY = productD,
                ProductYReqdQty = 1,
                FixedPrice = 30
            });
        }
    }
}
